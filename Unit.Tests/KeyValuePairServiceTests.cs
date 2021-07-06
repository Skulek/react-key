using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using backend.Dtos;
using backend.Mappings;
using backend.Repositories;
using backend.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Unit.Tests
{
   
    public class KeyValuePairServiceTests
    {
        private readonly IKeyValuePairService keyValuePairService;
        private readonly IKeyValuePairRepository keyValuePairRepository;
        private readonly MapperConfiguration mapperConfiguration;

        public KeyValuePairServiceTests()
        {
            keyValuePairRepository = Substitute.For<IKeyValuePairRepository>();
            mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new KeyValuePairMappingProfile()));
            mapperConfiguration.AssertConfigurationIsValid();
            keyValuePairService = new KeyValuiePairService(keyValuePairRepository, mapperConfiguration.CreateMapper());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        public void WHEN_user_asks_for_keyValuePairs_THEN_should_return_all_orders(int numberOfItems)
        {
            //arrange
            keyValuePairRepository.GetKeyValuePairs().Returns(CreateRandomKeyValuePairs(numberOfItems));

            //act
            var kvps = keyValuePairService.GetKeyValuePairs();

            //assert
            kvps.Should().HaveCount(numberOfItems);
        }


        [Fact]
        public void WHEN_user_inserts_keyValuePair_successfully_THEN_should_return_true()
        {
            //arrange
            keyValuePairService.InsertKeyValuePair(Substitute.For<KeyValuePairDto>()).ReturnsForAnyArgs(true);

            //act
            var kvps = keyValuePairService.InsertKeyValuePair(CreateRandomKeyValuePair());

            //assert
            kvps.Should().BeTrue();
        }

        private KeyValuePairDto CreateRandomKeyValuePair()
        {
            return new KeyValuePairDto
            {
                Id = Faker.RandomNumber.Next(0, 9),
                Key = Faker.Lorem.GetFirstWord(),
                Value = Faker.Lorem.GetFirstWord()
            };
        }

        private IEnumerable<backend.Models.KeyValuePair> CreateRandomKeyValuePairs(int numberOfItems)
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                yield return new backend.Models.KeyValuePair
                {
                    Id = i + 1,
                    Key = Faker.Lorem.GetFirstWord(),
                    Value = Faker.Lorem.GetFirstWord(),
                };
            }
            yield break;
        }
    }
    
}
