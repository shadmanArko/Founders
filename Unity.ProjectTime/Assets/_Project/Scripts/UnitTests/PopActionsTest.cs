using System;
using _Project.Scripts.Pop_Clan_Culture;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using Zenject;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class PopActionTest : ZenjectUnitTestFixture
{
    private SaveDataScriptableObject _saveDataScriptableObject;
    [SetUp]
    public void CommonInstall()
    {
        _saveDataScriptableObject = new SaveDataScriptableObject();
        Container.Bind<SaveDataScriptableObject>().FromInstance(_saveDataScriptableObject).AsSingle();
        Container.Bind<PopManager>().AsSingle();
    }

    [Test]
    public void TestInitialValues()
    {
        var popManager = Container.Resolve<PopManager>();

        Assert.That(popManager.CheckPop("PingPOng") == "PingPOng");
    }
    // [Test]
    // public void TestPopRemoveFromBuilding()
    // {
    //     var popManager = Container.Resolve<PopManager>();
    //     var buildingCoreData = new BuildingCoreData();
    //     var buildingVariation = new BuildingVariation();
    //     Building newBuilding = new Building(
    //         id: "fdf");
    //     Assert.That(popManager.RemovePopFromBuilding("PingPOng", "popId") );
    // }

   
}