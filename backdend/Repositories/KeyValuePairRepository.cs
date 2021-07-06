using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using backend.DbContext;

namespace backend.Repositories
{
    public class KeyValuePairRepository : IKeyValuePairRepository
    {
        private readonly IMapper mapper;
        private readonly KeyValuePairDbContext keyValuePairDbContext;

        public KeyValuePairRepository(IMapper mapper, KeyValuePairDbContext orderDbContext)
        {
            this.mapper = mapper;
            this.keyValuePairDbContext = orderDbContext;
        }

        public bool DeleteKeyValuePair(int keyValuePairId)
        {
            var kvp = keyValuePairDbContext.KeyValuePairs.Find(keyValuePairId);
            if(kvp == null)
            {
                throw new Exception();
            }
            keyValuePairDbContext.KeyValuePairs.Remove(kvp);
            return keyValuePairDbContext.SaveChanges() == 1;
        }

        public IEnumerable<Models.KeyValuePair> GetKeyValuePairs()
        {
            var keyValueEntities = keyValuePairDbContext.KeyValuePairs.AsQueryable();
            var keyValueModels = mapper.Map<IEnumerable<Models.KeyValuePair>>(keyValueEntities);
            return keyValueModels;
        }

        public bool InsertKeyValuePair(Models.KeyValuePair keyValuePair)
        {
            var keyValueEntity = mapper.Map<Entities.KeyValuePair>(keyValuePair);
            keyValuePairDbContext.KeyValuePairs.Add(keyValueEntity);
            return keyValuePairDbContext.SaveChanges() == 1;
        }

        public Models.KeyValuePair UpdateKeyValuePair(Models.KeyValuePair keyValuePair)
        {
            var kvp = keyValuePairDbContext.KeyValuePairs.Find(keyValuePair.Id);
            kvp = mapper.Map<Entities.KeyValuePair>(keyValuePair);
            keyValuePairDbContext.KeyValuePairs.Update(kvp);
            keyValuePairDbContext.SaveChanges();
            return keyValuePair;
        }
    }
}
