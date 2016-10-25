using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AsyncTest.Pump
{
    public static class TaskExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Ignore(this Task task)
        {
        }
    }
}