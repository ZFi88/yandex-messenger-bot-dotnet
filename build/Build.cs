using Bimlab.Nuke;
using Bimlab.Nuke.Components;

class Build : BimLabBuild, IPublish, ITest
{
    public static int Main() => Execute<Build>(x => x.From<ICompile>().Compile);
}