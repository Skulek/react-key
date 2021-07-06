using System.Collections.Generic;
using backend.Dtos;

namespace backend.Services
{
    public interface IKeyValuePairService
    {
        bool InsertKeyValuePair(KeyValuePairDto keyValuePair);
        IEnumerable<KeyValuePairDto> GetKeyValuePairs();
        bool DeleteKeyValuePair(int kvpId);
        KeyValuePairDto UpdateKeyValuePair(KeyValuePairDto keyValuePair);
    }
}