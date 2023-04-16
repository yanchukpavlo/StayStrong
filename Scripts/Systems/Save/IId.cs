
namespace Game.Systems.Save
{
    public interface IId
    {
        public static string GetId()
        {
            return System.Guid.NewGuid().ToString();
        }
        
        public string Id { get; }
    }
}