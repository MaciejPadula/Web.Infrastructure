namespace ServerTest.Repository
{
    public class DbSet<T>
    {
        private readonly Dictionary<int, T> _list = new();

        public int Add(T t, Action<int, T> keySetter)
        {
            var newKey = _list.Keys.LastOrDefault() + 1;
            keySetter.Invoke(newKey, t);
            _list.Add(newKey, t);
            return newKey;
        }

        public List<T> Get()
        {
            return _list.Values.ToList();
        }
    }
}
