var group = new List<User>().GroupBy(x => x.FirstName);
var groupKeys = group.Select(g => {
    var (k, v) = g.DeconstructGroup();
    return (k, v);
});

record User(string FirstName, string LastName);
public static class GroupingExtensions
{
    public static (T key, IEnumerable<TG> group) DeconstructGroup<T, TG>(this IGrouping<T, TG> group)
        => (group.Key, group);
}