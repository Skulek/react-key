using System;
using System.Collections.Generic;
using AutoMapper;
using backend.Dtos;
using backend.Repositories;

namespace backend.Services
{
    public class KeyValuiePairService: IKeyValuePairService
    {
        private readonly IKeyValuePairRepository keyValuePairRepository;
        private readonly IMapper mapper;

        public KeyValuiePairService(IKeyValuePairRepository keyValuePairRepository, IMapper mapper)
        {
            this.keyValuePairRepository = keyValuePairRepository;
            this.mapper = mapper;
        }

        public bool InsertKeyValuePair(KeyValuePairDto keyValuePair)
        {
            var kvpModel = mapper.Map<Models.KeyValuePair>(keyValuePair);
            return keyValuePairRepository.InsertKeyValuePair(kvpModel);
        }
        public IEnumerable<KeyValuePairDto> GetKeyValuePairs()
        {
            var kvpEnities = keyValuePairRepository.GetKeyValuePairs();
            return mapper.Map<IEnumerable<KeyValuePairDto>>(kvpEnities);
        }

        public bool DeleteKeyValuePair(int kvpId)
        {
            throw new NotImplementedException();
        }

        public KeyValuePairDto UpdateKeyValuePair(KeyValuePairDto keyValuePair)
        {
            throw new NotImplementedException();
        }
    }
}
