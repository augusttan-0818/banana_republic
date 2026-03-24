
namespace NRC.Const.CodesAPI.Domain.Entities.Core
{
    public class CodesLifecycleStage
    {
        public required byte LifecycleStageId {  get; set; }

        public required string LifecycleStageName {  get; set; }

        public string? Description {  get; set; }
    }
}

