namespace NRC.Const.CodesAPI.Domain.Entities.Core
{
    public class CodesBook
    {
        public required short CodesBookId { get; set; }

        public required string Name { get; set; }

        public required string ShortName { get; set; }

        public CodesCycle? CodeCycle {  get; set; }

        public required short CodesCycleId { get; set; }
    }
}

