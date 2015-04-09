namespace CoreTests
{
    using Core;

    using Xunit;

    public class FrameworkTests
    {
        [Fact]
        public void CreateInitializeGameStateCreatesAValidGameState()
        {
            var state = Framework.CreateInitializeGameState(null, null, null);
            Assert.NotNull(state);
        }
    }
}
