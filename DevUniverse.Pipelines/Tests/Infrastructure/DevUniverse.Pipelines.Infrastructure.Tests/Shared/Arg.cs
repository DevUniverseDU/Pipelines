namespace DevUniverse.Pipelines.Infrastructure.Tests.Shared
{
    public record Arg
    {
        public int Property { get; set; } = 0;

        public static Arg operator +(Arg param0, int param1) => new Arg()
        {
            Property = param0.Property + param1
        };

        public static Arg operator -(Arg param0, int param1) => new Arg()
        {
            Property = param0.Property - param1
        };

        public static Arg operator *(Arg param0, Arg param1) => new Arg()
        {
            Property = param0.Property * param1.Property
        };
    }
}
