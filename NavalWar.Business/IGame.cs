using NavalWar.DTO;

namespace NavalWar.Business
{
    public interface IGame
    {
        int getdata(int id);
        public int IdGame { get; }
        public Map[] ListMap { get; }
    }
}