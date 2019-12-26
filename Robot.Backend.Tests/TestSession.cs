using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Robot.Backend.Tests
{
    public class TestSession : ISession
    {
        readonly Dictionary<string, byte[]> _state = new Dictionary<string, byte[]>();

        public string Id => throw new System.NotImplementedException();

        public bool IsAvailable => throw new System.NotImplementedException();

        public IEnumerable<string> Keys => throw new System.NotImplementedException();

        public void Clear() => _state.Clear();
    
        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key) => _state.Remove(key);
     
        public void Set(string key, byte[] value) => _state[key] = value;
      
        public bool TryGetValue(string key, out byte[] result)
        {
            if (_state.TryGetValue(key, out var value)) {
                 result = value;
                return true;
            }

            result = null;
            return false;
        }
    }

}