namespace FileBaseContext.Context.Extensions;

/// <summary>
/// Provides methods for relfection related operations
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    /// Determines whether type A is child of type B
    /// </summary>
    /// <param name="child">Type checked as child</param>
    /// <param name="parent">Type checked as parent</param>
    /// <returns>Result of the check, true if is child</returns>
    public static bool InheritsOrImplements(this Type child, Type parent)
    {
        var par = parent;
        return InheritsOrImplementsHalf(child, ref parent) || par.IsAssignableFrom(child);
    }

    /// <summary>
    /// Determines whether type A inherits or implements type B
    /// </summary>
    /// <param name="child">Type checked as derived or implementing type</param>
    /// <param name="parent">Type checked as parent or interface type</param>
    /// <returns>Result of the check, true if does inherit or implement</returns>
    private static bool InheritsOrImplementsHalf(this Type child, ref Type parent)
    {
        parent = ResolveGenericTypeDefinition(parent);
        var currentChild = child.IsGenericType
                               ? child.GetGenericTypeDefinition()
                               : child;
        while (currentChild != typeof(object))
        {
            if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
                return true;
            currentChild = currentChild.BaseType != null
                           && currentChild.BaseType.IsGenericType
                               ? currentChild.BaseType.GetGenericTypeDefinition()
                               : currentChild.BaseType;
            if (currentChild == null)
                return false;
        }
        return false;
    }

    /// <summary>
    /// Determines whether type A implements type B as direct interface
    /// </summary>
    /// <param name="child">Type checked as implementing type</param>
    /// <param name="parent">Type checked asinterface type</param>
    /// <returns>Result of the check, true if does implement</returns>
    private static bool HasAnyInterfaces(Type parent, Type child)
    {
        return child.GetInterfaces()
            .Any(childInterface =>
            {
                var currentInterface = childInterface.IsGenericType
                    ? childInterface.GetGenericTypeDefinition()
                    : childInterface;

                return currentInterface == parent;
            });
    }

    /// <summary>
    /// Gets generic type definition from a type
    /// </summary>
    /// <param name="parent">Type being resolved</param>
    /// <returns>Generic type</returns>
    private static Type ResolveGenericTypeDefinition(Type parent)
    {
        var shouldUseGenericType = !(parent.IsGenericType && parent.GetGenericTypeDefinition() != parent);
        if (parent.IsGenericType && shouldUseGenericType)
            parent = parent.GetGenericTypeDefinition();
        return parent;
    }
}