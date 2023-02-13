using System.Collections.Generic;
using System.IO;
using MECS.Collections;
using MECS.MemoryManagement;
using MECS.Patrons.Commands;
using MECS.Tools;
using MECS.Variables.References;
using UnityEditor;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Scriptable object used to set systems, managers and other stuff configurations.
    [CreateAssetMenu(fileName = "New_MECS_Settings", menuName = "MECS/Patrons/Settings")]
    public class MECSSettings : ScriptableObject
    {
        //Editor variables
        [Header("Configurations")]
        [SerializeField] private ScriptableObject[] systems = null;
        [SerializeField] private ScriptableObject[] managers = null;
        [SerializeField] private bool activeSettings = true;
        [SerializeField] private BoolReference activeDebug = null;

        //Variables
        private static bool areSettingsLoaded = false,
        canDebug = true;

        //Store systems and managers references
        //T = string = Type
        //T2 = ScriptableObject
        private static Dictionary<string, ScriptableObject> systemsDictionary = null,
        managersDictionary = null,
        scriptableObjectDictionary = null,
        inputScriptableDictionary = null;

        //Attributes
        public static bool AreSettingsLoaded => areSettingsLoaded;
        public static Dictionary<string, ScriptableObject> SystemsDictionary
        {
            //Return system dictionary or load it and store
            get
            {
                //Check dictionary
                if (!ReferenceTools.IsValueSafe(systemsDictionary))
                    systemsDictionary = new LoadScriptableObjectsCommand(
                        MECSDefaultSettingsNaming.systemsNaming.DEFAULT_SYSTEMS_SHORT_PATH).Execute();

                return systemsDictionary;
            }
        }
        public static Dictionary<string, ScriptableObject> ManagersDictionary => managersDictionary;
        public static Dictionary<string, ScriptableObject> ScriptableObjectDictionary => scriptableObjectDictionary;
        public static Dictionary<string, ScriptableObject> InputScriptableDictionary
        {
            //Return input scriptable dictionary or load it and store
            get
            {
                //Check dictionary
                if (!ReferenceTools.IsValueSafe(inputScriptableDictionary))
                {
                    //Load mouse assets
                    Dictionary<string, ScriptableObject> tempMouseAssets = new LoadScriptableObjectsCommand(
                        MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming
                        .mouseInputScriptableObjectsNaming.DEFAULT_MOUSE_INPUT_SHORT_PATH).Execute(),

                    //Load keyboard assets
                    tempKeyboardAssets = new LoadScriptableObjectsCommand(
                        MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming
                        .keyboardInputScriptableObjectsNaming.DEFAULT_KEYBOARD_INPUT_SHORT_PATH).Execute();

                    //Merge dictionaries and set on inputScriptableDictionary
                    if (CollectionsTools.dictionaryTools.MergeDictionaries(tempMouseAssets, tempKeyboardAssets,
                    out Dictionary<string, ScriptableObject> mergerMouseKeyboardDictionary, " couldnt merge loaded input assets"))
                        inputScriptableDictionary = mergerMouseKeyboardDictionary;
                }

                return inputScriptableDictionary;
            }
        }
        public bool ActiveDebug => activeDebug.Value;
        public static bool CanDebug => canDebug;

        //Method
        //Call after scene loads, loads also MECS settings if its necessary, 
        //Instantiating an ScriptableLoader with all referenced scriptableObjects
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InstantiateScriptableLoader()
        {
            //Check if its already loaded
            if (!areSettingsLoaded)
            {
                //Debug information
                BasicDebugInformation basicDebugInformation = new BasicDebugInformation("MECSSettings", "InstantiateScriptableLoader()");

                //Load settings
                MECSSettings[] settings = Resources.LoadAll<MECSSettings>(MECSDefaultSettingsNaming.DEFAULT_SETTINGS_SHORT_PATH);

                //Avoid errors checking settings array
                bool isArraySettingsSafe = CollectionsTools.arrayTools.
                IsArrayContentSafe(settings, " settings array isn't safe");

                //Use settings
                //Set target settings based on settings array, later set values on loader instance
                if (isArraySettingsSafe)
                {
                    //Itinerate and store correct setting scriptable
                    MECSSettings targetSettings = null;

                    foreach (MECSSettings setting in settings)
                    {
                        //Check if its active
                        if (setting.activeSettings)
                        {
                            //Check if there is other active settings
                            if (!targetSettings)
                                targetSettings = setting;
                            //Notify to debug manager
#if UNITY_EDITOR
                            else
                                Debug.LogError(" there is more than one active settings");
#endif
                        }
                    }

                    //Check if there is any active setting
                    if (ReferenceTools.IsValueSafe(targetSettings, " there isn't any active settings"))
                    {
                        //Store systems and managers
                        ScriptableObject[] systems = targetSettings.systems,
                        managers = targetSettings.managers;

                        //Check if arrays are safe
                        bool isSystemsArraySafe = CollectionsTools.arrayTools.
                        IsArrayContentSafe(systems, " systems editor array isn't safe"),
                        isManagersArraySafe = CollectionsTools.arrayTools.
                        IsArrayContentSafe(managers, " managers editor array isn't safe"),
                        areArraysSafe = isSystemsArraySafe && isManagersArraySafe;

                        //Merge arrays and create loader
                        if (areArraysSafe)
                        {
                            //Add all scriptableObjects
                            List<ScriptableObject> scriptableList = new List<ScriptableObject>();
                            scriptableList.AddRange(systems);
                            scriptableList.AddRange(managers);

                            ScriptableLoaderComponent.Instance.loadedScriptables = scriptableList.ToArray();

                            areSettingsLoaded = true;
                            canDebug = targetSettings.activeDebug.Value;
                        }
#if UNITY_EDITOR
                        //Information of errors
                        else
                        {
                            //Debug system array
                            if (!isSystemsArraySafe)
                                DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                                    "setting assets doesnt have systems on " + MECSDefaultSettingsNaming.systemsNaming.DEFAULT_SYSTEMS_PATH +
                                    ", reset configuration on MECS/Setting/Reset settings or configure it manually"));

                            //Debug manager array
                            if (!isManagersArraySafe)
                                DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                                    "setting assets doesnt have managers on " + MECSDefaultSettingsNaming.managersNaming.DEFAULT_MANAGERS_PATH +
                                    ", reset configuration on MECS/Setting/Reset settings or configure it manually"));
                        }
#endif
                    }
#if UNITY_EDITOR
                    else
                        DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                             "settings assets doesnt exists on " + MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH +
                             ", reset configuration on MECS/Setting/Reset settings"));
#endif
                }
#if UNITY_EDITOR
                else
                    DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                             "settings assets doesnt exists on " + MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH +
                             ", reset configuration on MECS/Setting/Reset settings"));
#endif 
            }
        }

#if UNITY_EDITOR
        //Method
        //Generate MECS default configuration   
        [MenuItem("MECS/Settings/Reset all settings")]
        private static void ResetSettings()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new BasicDebugInformation("MECSSettings", "ResetSettings()");

            //Check if folder exists and delete all framework content
            if (MECSAssetsTools.CheckAllAssets())
            {
                //Get root directory
                DirectoryInfo rootDirectory = new DirectoryInfo(MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH);

                //Delete content
                rootDirectory.Delete(true);

                AssetDatabase.Refresh();

                //Debug delete of content
                DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "deleted all framework content."));
            }

            //Create folders tree
            if (new CreateMECSFolderTreeCommand().Execute())
            {
                AssetDatabase.Refresh();

                //Debug creation of folder
                Debug.Log("created root folder as " + MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH + ".");

                //Create systems
                Dictionary<string, ScriptableObject> tempSystemsDictionary =
                new CreateMECSSystemsCommand().Execute(),

                //Create managers
                tempManagersDictionary = new CreateMECSManagersCommand().Execute(),

                //Create scriptable objects
                tempScriptableObjectsDictionary = new CreateMECSScriptableObjectsCommand().Execute(),

                //Create input assets
                tempInputScriptableDictionary = new CreateMECSInputScriptableCommands().Execute();

                //Avoid errors based on stored dictionaries
                bool areSystemsDictionaryValuesSafe =
                //Get systems
                CollectionsTools.dictionaryTools.GetAllValues(tempSystemsDictionary, out List<ScriptableObject> systemsValues,
                " systems dictionary values aren't safe"),
                //Get managers
                areManagersDictionaryValuesSafe = CollectionsTools.dictionaryTools.GetAllValues(tempManagersDictionary,
                out List<ScriptableObject> managersValues, " managers dictionary values aren't safe"),
                //Get scriptable
                areScriptableDictionaryValuesSafe = CollectionsTools.dictionaryTools.GetAllValues(tempScriptableObjectsDictionary,
                out List<ScriptableObject> scriptableValues, " scriptable dictionary values aren't safe"),
                //Get inputs
                areInputDictionaryValuesSafe = CollectionsTools.dictionaryTools.GetAllValues(tempInputScriptableDictionary,
                out List<ScriptableObject> inputValues, " input dictionary values aren't safe"),
                //Summarize
                areAllValuesSafe = areSystemsDictionaryValuesSafe
                && areManagersDictionaryValuesSafe
                && areScriptableDictionaryValuesSafe
                && areInputDictionaryValuesSafe;

                //Check if can use values
                if (areAllValuesSafe)
                {
                    //Add to static dictionaries temp ranges
                    systemsDictionary = tempSystemsDictionary;
                    managersDictionary = tempManagersDictionary;
                    scriptableObjectDictionary = tempScriptableObjectsDictionary;
                    inputScriptableDictionary = tempInputScriptableDictionary;

                    //Store values from dictionaries
                    ScriptableObject[] systemsAssets = systemsValues.ToArray();
                    ScriptableObject[] managersAssets = managersValues.ToArray();

                    //Instantiate
                    MECSSettings settingsAsset = ScriptableObject.CreateInstance<MECSSettings>();

                    //Create asset and save it
                    AssetDatabase.CreateAsset(settingsAsset, MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH
                    + "/Default_MECS_Settings.asset");
                    AssetDatabase.SaveAssets();

                    //Set settings configuration
                    settingsAsset.systems = systemsAssets;
                    settingsAsset.managers = managersAssets;

                    //End settings configuration
                    if (new ConfigureMECSInputCommand(
                        new ComplexDebugInformation(basicDebugInformation, "couldnt configure input system")).Execute())
                    {
                        //Debug creation of settings
                        DebugTools.
                        DebugLog(new ComplexDebugInformation(basicDebugInformation,
                        "created settings asset as Default_MECS_Settings.asset on " + MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH + "."));

                        //Debug end operation
                        DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "settings reset finished."));
                    }
                }
                else
                    //Debug end operation error
                    DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "settings reset couldnt be make"));
            }
        }

        //         //Method
        //         //Generate MECS default systems configuration
        //         [MenuItem("MECS/Settings/Reset systems")]
        //         private static void ResetSystems()
        //         {
        //             //Debug information
        //             BasicDebugInformation basicDebugInformation = new BasicDebugInformation("MECSSettings", "ResetSystems()");

        //             //Check if folder exists and delete all systems content
        //             if (AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.systemsNaming.DEFAULT_SYSTEMS_PATH))
        //             {
        //                 //Get root directory and delete related content
        //                 DirectoryInfo systemsDirectory = new DirectoryInfo(MECSDefaultSettingsNaming.systemsNaming.DEFAULT_SYSTEMS_PATH);

        //                 systemsDirectory.Delete(true);

        //                 AssetDatabase.Refresh();

        //                 systemsDictionary.Clear();

        //                 //Debug delete of content
        // #if UNITY_EDITOR
        //                 DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "deleted all systems content."));
        // #endif
        //             }

        //             //Create folder
        //             AssetDatabase.CreateFolder("Assets/Resources/MECS", "Systems");

        //             AssetDatabase.Refresh();

        // #if UNITY_EDITOR
        //             DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "created folder for systems content."));
        // #endif

        //             //Store systems on settings dictionary
        //             //systemsDictionary = new CreateMECSSystemsCommand().Execute();

        //             //Debug end operation
        // #if UNITY_EDITOR
        //             DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "systems reset finished."));
        // #endif
        //         }

        //         //Method
        //         //Generate MECS default managers configuration 
        //         [MenuItem("MECS/Settings/Reset managers")]
        //         public static void ResetManagers()
        //         {
        //             //Debug information
        //             BasicDebugInformation basicDebugInformation = new BasicDebugInformation("MECSSettings", "ResetSystems()");

        //             //Check if folder exists and delete all managers content
        //             if (AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.managersNaming.DEFAULT_MANAGERS_PATH))
        //             {
        //                 //Get root directory
        //                 DirectoryInfo managersDirectory = new DirectoryInfo(MECSDefaultSettingsNaming.managersNaming.DEFAULT_MANAGERS_PATH);

        //                 managersDirectory.Delete(true);

        //                 AssetDatabase.Refresh();

        //                 managersDictionary.Clear();

        //                 //Debug delete of content
        // #if UNITY_EDITOR
        //                 DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "deleted all managers content."));
        // #endif
        //             }

        //             //Create folder
        //             AssetDatabase.CreateFolder("Assets/Resources/MECS", "Managers");

        //             AssetDatabase.Refresh();

        // #if UNITY_EDITOR
        //             DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "created folder for managers content."));
        // #endif

        //             //Create all managers assets and store
        //             //managersDictionary = new CreateMECSManagersCommand().Execute();

        //             //Safe assets
        //             AssetDatabase.SaveAssets();

        //             AssetDatabase.Refresh();

        //             //Debug end operation
        // #if UNITY_EDITOR
        //             DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "managers reset finished."));
        // #endif
        //         }

        //         //Method
        //         //Generate MECS default managers configuration
        //         [MenuItem("MECS/Settings/Reset scriptable objects")]
        //         private static void ResetScriptableObjects()
        //         {
        //             //Debug information
        //             BasicDebugInformation basicDebugInformation = new BasicDebugInformation("MECSSettings", "ResetScriptableObjects()");

        //             //Check if folder exists and delete all scriptableObjects content
        //             if (AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_PATH))
        //             {
        //                 //Get root directory
        //                 DirectoryInfo scriptableObjectsDirectory = new DirectoryInfo(MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_PATH);

        //                 scriptableObjectsDirectory.Delete(true);

        //                 AssetDatabase.Refresh();

        //                 //Debug delete of content
        // #if UNITY_EDITOR
        //                 DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "deleted all scriptable objects content."));
        // #endif
        //             }

        //             //Create folder
        //             AssetDatabase.CreateFolder("Assets/Resources/MECS", "Scriptable_Objects");

        //             AssetDatabase.Refresh();

        // #if UNITY_EDITOR
        //             DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "created folder for scriptable objects content."));
        // #endif

        //             //Create all scriptable objects assets and store
        //             //new CreateMECSScriptableObjectsCommand().Execute();

        //             //Safe assets
        //             AssetDatabase.SaveAssets();

        //             AssetDatabase.Refresh();

        //             //Debug end operation
        // #if UNITY_EDITOR
        //             DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "scriptable objects reset finished."));
        // #endif
        //         }

        //         //Method
        //         //Generate MECS default managers configuration
        //         [MenuItem("MECS/Settings/Reset scriptable objects")]
        //         private static void ResetInputs()
        //         {
        //             //Debug information
        //             BasicDebugInformation basicDebugInformation = new BasicDebugInformation("MECSSettings", "ResetInputs()");

        //             //Check if folder exists and delete all pc input content
        //             if (AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.inputScriptableObjectsNaming.DEFAULT_INPUT_PATH))
        //             {
        //                 //Get root directory
        //                 DirectoryInfo pcInputDirectory = new DirectoryInfo(MECSDefaultSettingsNaming.inputScriptableObjectsNaming.DEFAULT_INPUT_PATH);

        //                 pcInputDirectory.Delete(true);

        //                 AssetDatabase.Refresh();

        //                 //Debug delete of content
        // #if UNITY_EDITOR
        //                 DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "deleted all input content."));
        // #endif
        //             }

        //             //Create input folder
        //             AssetDatabase.CreateFolder("Assets/Resources/MECS", "Input");

        //             AssetDatabase.Refresh();

        //             //Create PC input folder
        //             AssetDatabase.CreateFolder("Assets/Resources/MECS/Input", "PC");

        //             AssetDatabase.Refresh();

        // #if UNITY_EDITOR
        //             DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "created folder for input content."));
        // #endif

        //             //Create all scriptable objects assets and store
        //             //new CreateMECSInputScriptableCommands().Execute();

        //             //Safe assets
        //             AssetDatabase.SaveAssets();

        //             AssetDatabase.Refresh();

        //             //Debug end operation
        // #if UNITY_EDITOR
        //             DebugTools.DebugLog(new ComplexDebugInformation(basicDebugInformation, "input reset finished."));
        // #endif
        //         }

        //Method
        //Configure input system values
        [MenuItem("MECS/Settings/Configure input system")]
        private static void ConfigureInputSystem() =>
        new ConfigureMECSInputCommand(
            new ComplexDebugInformation("MECSSettings", "ConfigureInputSystem()", "couldnt configure input system"))
        .Execute();
#endif
    }
}