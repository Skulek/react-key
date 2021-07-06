using System.Collections.Generic;

namespace backend.Repositories
{
    public interface IKeyValuePairRepository
    {
        bool InsertKeyValuePair(Models.KeyValuePair keyValuePair);
        bool DeleteKeyValuePair(int kvpId);
        Models.KeyValuePair UpdateKeyValuePair(Models.KeyValuePair keyValuePair);
        IEnumerable<Models.KeyValuePair> GetKeyValuePairs();
    }
}
