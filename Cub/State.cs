namespace Cub
{
    public class State : CObject
    {
        public State()
        {
        }

        public State(State obj)
            : base(obj)
        {
        }

        public static State Get(string id)
        {
            return BaseGet<State>(id, null);
        }

        public string Name => _string("name");

        public State Reload()
        {
            BaseReload();
            return this;
        }
    }
}
