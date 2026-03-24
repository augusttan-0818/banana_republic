namespace NRC.Const.CodesAPI.Domain.Entities.Core
{
    public class CodesStrategicPriority
    {
        public required short StrategicPriorityId { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public required short CodesCycleId { get; set; }

        public CodesCycle? CodesCycle { get; set; }
    }
}

