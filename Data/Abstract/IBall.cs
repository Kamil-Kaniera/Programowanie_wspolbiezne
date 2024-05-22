namespace Data.Abstract
{
    public interface IBall : IObservable<IBall>, IDisposable
    {
        Position Position { get; }
        VelocityVector Velocity { get; set; }
    }
}
