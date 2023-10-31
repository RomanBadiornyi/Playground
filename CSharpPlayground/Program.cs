var group = new List<User>().GroupBy(x => x.FirstName);
var groupKeys1 = group.Select(g => {
    var (k, v) = g.DeconstructGroup1();
    return (k, v);
});

var groupKeys2 = group.Select(g => {
    var (k, v) = g;
    return (k, v);
});

record User(string FirstName, string LastName);
public static class GroupingExtensions
{
    public static (T key, IEnumerable<TG> group) DeconstructGroup1<T, TG>(this IGrouping<T, TG> group)
        => (group.Key, group);
    
    public static void Deconstruct<T, TG>(this IGrouping<T, TG> group, out T key, out IEnumerable<TG> collection)
        => (key, collection) = (group.Key, group);
}