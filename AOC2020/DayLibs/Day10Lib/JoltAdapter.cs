using System;

namespace AOC2020.DayLibs.Day10Lib
{

    public interface IPowerRated
    {
        public int Rating { get; set; }
        public int inDiff { get; }
    }

    public interface IPowerSource : IPowerRated
    {
        public IPowerRecipient outConn { get; set; }
    }

    public interface IPowerRecipient : IPowerRated
    {
        public IPowerSource inConn { get; set; }
        public void ConnectSource(IPowerSource src);
    }

    public class WallOutlet : IPowerRated, IPowerSource
    {
        public int Rating { get; set; }
        public IPowerRecipient outConn { get; set; }
        public int inDiff { get; } = 0;
        public WallOutlet(int rating=0)
        {
            Rating = rating;
        }
    }

    public class Device : IPowerRecipient
    {
        public IPowerSource inConn { get; set; }
        public int Rating { get; set; }
        public int inDiff => this.Rating - inConn.Rating;

        public Device(int rating)
        {
            Rating = rating;
        }

        public void ConnectSource(IPowerSource src)
        {
            inConn = src;
            src.outConn = this;
        }
    }

    public class JoltAdapter : IPowerSource, IPowerRecipient
    {
        public int Rating { get; set; }
        public IPowerSource inConn { get; set; } = new WallOutlet();
        public IPowerRecipient outConn { get; set; } = new Device(0);
        public int inDiff
        {
            get
            {
                return this.Rating - inConn.Rating;
            }
        }

        public JoltAdapter(int rating)
        {
            Rating = rating;
        }

        public void ConnectSource(IPowerSource src)
        {
            if(src.Rating > this.Rating)
            {
                throw new ArgumentException($"The adapter being connected has a " +
                                            $"higher rating than this adapter: { src.Rating } > { Rating }");
            }
            inConn = src;
            src.outConn = this;
        }

        public void ConnectRecipient(IPowerRecipient device)

        {
            outConn = device;
            device.inConn = this;
        }

        public override string ToString()
        {
            return $"<JoltAdapter:: Rating={Rating}, inDiff={inDiff}>";
        }
    }
}
