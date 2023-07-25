namespace Jaxis.Interfaces
{
    public enum PosStatus
    {
        Alert = 1,
        Pending = 2,
        Complete = 4,
        Void = 8,
        UnknownAlias = 16,
        UnknownRecipe = 32,
        UnderPour = 64,
        OverPour = 128,
        Substitution = 256,
        Combined = 512,
    }
}
