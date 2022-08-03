using System.Runtime.Serialization;
using Iviz.Roslib.Utils;
using Iviz.Roslib2.Rcl;

namespace Iviz.Roslib2;

[DataContract]
public sealed class QosProfile : JsonToString, IEquatable<QosProfile>
{
    internal readonly RmwQosProfile Profile;

    public HistoryPolicy History => Profile.History;
    public long Depth => Profile.Depth;
    public ReliabilityPolicy Reliability => Profile.Reliability;
    public DurabilityPolicy Durability => Profile.Durability;

    internal QosProfile(in RmwQosProfile profile)
    {
        Profile = profile;
    }

    public QosProfile(HistoryPolicy history, int depth, ReliabilityPolicy reliability, DurabilityPolicy durability)
    {
        Profile = new RmwQosProfile(history, depth, reliability, durability);
    }

    public bool Equals(QosProfile? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Profile.Equals(other.Profile);
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is QosProfile other && Equals(other);
    public override int GetHashCode() => Profile.GetHashCode();
    public static bool operator ==(QosProfile? left, QosProfile? right) => Equals(left, right);
    public static bool operator !=(QosProfile? left, QosProfile? right) => !Equals(left, right);


    // ---------------------

    static QosProfile? sensorProfile;
    static QosProfile? parametersProfile;
    static QosProfile? defaultProfile;
    static QosProfile? servicesProfile;
    static QosProfile? systemProfile;

    public static QosProfile SensorData => sensorProfile ??= new QosProfile(
        HistoryPolicy.KeepLast,
        5,
        ReliabilityPolicy.BestEffort,
        DurabilityPolicy.Volatile
    );

    public static QosProfile Parameters => parametersProfile ??= new QosProfile(
        HistoryPolicy.KeepLast,
        1000,
        ReliabilityPolicy.Reliable,
        DurabilityPolicy.Volatile
    );

    public static QosProfile Default => defaultProfile ??= new QosProfile(
        HistoryPolicy.KeepLast,
        10,
        ReliabilityPolicy.Reliable,
        DurabilityPolicy.Volatile
    );

    public static QosProfile ServicesDefault => servicesProfile ??= new QosProfile(
        HistoryPolicy.KeepLast,
        1000,
        ReliabilityPolicy.Reliable,
        DurabilityPolicy.Volatile
    );

    public static QosProfile SystemDefault => systemProfile ??= new QosProfile(
        HistoryPolicy.SystemDefault,
        RmwQosProfile.DepthSystemDefault,
        ReliabilityPolicy.SystemDefault,
        DurabilityPolicy.SystemDefault
    );
}