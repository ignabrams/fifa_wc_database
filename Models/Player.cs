namespace Models
{
    public class Player
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public short JerseyNumber { get; set; }
        public short Age { get; set; }
        public Stats Stats { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }
        public Position Position { get; set; }
        public IEnumerable<PlayerMatch> PlayerMatches { get; set; }
    }
}