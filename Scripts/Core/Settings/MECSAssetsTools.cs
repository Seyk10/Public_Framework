#if UNITY_EDITOR
using UnityEditor;
using MECS.Tools;
using static MECS.Tools.DebugTools;
using UnityEngine;

namespace MECS.Core
{
    //* Tools used to check folders related to MECS
    public static class MECSAssetsTools
    {
        //Check all assets related to framework
        public static bool CheckAllAssets()
        {
            //Return value
            bool isRootFolderValid = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH),
            areSystemsFoldersValid = CheckSystemsAssets(),
            areManagersFoldersValid = CheckManagersAssets(),
            areScriptableObjectFoldersValid = CheckScriptableObjectsAssets(),
            areInputFoldersValid = CheckInputFolder(),
            areAllFoldersOkay = isRootFolderValid && areSystemsFoldersValid && areManagersFoldersValid
            && areScriptableObjectFoldersValid && areInputFoldersValid;

#if UNITY_EDITOR
            //Debug errors
            //Root folder
            if (!isRootFolderValid)
                DebugTools.DebugError(new ComplexDebugInformation("MECSAssetsTools", "CheckAllAssets()", "root folder is not valid"));
#endif

            return areAllFoldersOkay;
        }

        //Check all assets related to systems framework
        //TODO: Add checking of asset plus folders
        public static bool CheckSystemsAssets()
        {
            //Return value
            bool areSystemFoldersOkay = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.systemsNaming.DEFAULT_SYSTEMS_PATH);

            //Debug errors
#if UNITY_EDITOR
            if (!areSystemFoldersOkay)
            {
                //Basic information to debug
                BasicDebugInformation basicDebugInformation = new("MECSAssetsTools", "CheckSystemsAssets()");

                DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Systems folder doesnt exists"));
            }
#endif

            return areSystemFoldersOkay;
        }

        //Check all assets related to managers framework
        //TODO: Add checking of asset plus folders
        public static bool CheckManagersAssets()
        {
            //Return value
            bool areManagersFoldersOkay = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.managersNaming.DEFAULT_MANAGERS_PATH);

            //Debug errors
#if UNITY_EDITOR
            if (!areManagersFoldersOkay)
            {
                //Basic information to debug
                BasicDebugInformation basicDebugInformation = new("MECSAssetsTools", "CheckManagersAssets()");

                DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Managers folder doesnt exists"));
            }
#endif

            return areManagersFoldersOkay;
        }

        //Check all assets related to managers framework
        //TODO: Add checking of asset plus folders
        public static bool CheckScriptableObjectsAssets()
        {
            //Return value, check each scriptable system folder
            bool existsScriptableObjectFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_PATH),
            existsAddresablePooledFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.addresablePooledScriptableObjectsNaming.DEFAULT_ADDRESABLE_POOLED_SCRIPTABLE_OBJECTS_PATH),
            existsCastingFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.castingScriptableObjectsNaming.DEFAULT_CASTING_SCRIPTABLE_OBJECTS_PATH),
            existsConditionalFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.conditionalsScriptableObjectsNaming.DEFAULT_CONDITIONALS_SCRIPTABLE_OBJECTS_PATH),
            existsFunctionalFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.functionalitiesScriptableObjectsNaming.DEFAULT_FUNCTIONALITIES_SCRIPTABLE_OBJECTS_PATH),
            existsGameEventFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.gameEventListenerScriptableObjectsNaming.DEFAULT_GAME_EVENT_LISTENER_SCRIPTABLE_OBJECTS_PATH),
            existsTimerFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.timerScriptableObjectsNaming.DEFAULT_TIMER_SCRIPTABLE_OBJECTS_PATH),
            areScriptableObjectsFoldersOkay = existsScriptableObjectFolder && existsAddresablePooledFolder && existsCastingFolder
            && existsConditionalFolder && existsFunctionalFolder && existsGameEventFolder && existsTimerFolder;

            //Debug errors
#if UNITY_EDITOR
            if (!areScriptableObjectsFoldersOkay)
            {
                //Basic information to debug
                BasicDebugInformation basicDebugInformation = new("MECSAssetsTools", "CheckScriptableObjectsAssets()");

                //Debug main folder
                if (!existsScriptableObjectFolder)
                    DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Scriptable Objects folder doesnt exist"));
                else
                {
                    //Check addresable pooled
                    if (!existsAddresablePooledFolder)
                        DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Addresable Pooled folder doesnt exist"));

                    //Check casting
                    if (!existsCastingFolder)
                        DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Casting folder doesnt exist"));

                    //Check addresable pooled
                    if (!existsConditionalFolder)
                        DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Conditional folder doesnt exist"));

                    //Check functional
                    if (!existsFunctionalFolder)
                        DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Functional folder doesnt exist"));

                    //Check game event
                    if (!existsGameEventFolder)
                        DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Game Event folder doesnt exist"));

                    //Check timer
                    if (!existsTimerFolder)
                        DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Timer folder doesnt exist"));
                }
            }
#endif

            return areScriptableObjectsFoldersOkay;
        }

        //Check all assets related to managers framework
        //TODO: Add checking of asset plus folders
        public static bool CheckInputFolder()
        {
            //Return value
            bool existsInputFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.inputScriptableObjectsNaming.DEFAULT_INPUT_PATH),
            existsPCFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming.DEFAULT_PC_INPUT_PATH),
            existsMouseFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming.mouseInputScriptableObjectsNaming.DEFAULT_MOUSE_INPUT_PATH),
            existsKeyboardFolder = AssetDatabase.IsValidFolder(MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming.keyboardInputScriptableObjectsNaming.DEFAULT_KEYBOARD_INPUT_PATH),
            areInputFoldersOkay = existsInputFolder && existsPCFolder && existsMouseFolder && existsKeyboardFolder;

            //Debug errors
#if UNITY_EDITOR
            if (!areInputFoldersOkay)
            {
                //Basic information to debug
                BasicDebugInformation basicDebugInformation = new("MECSAssetsTools", "CheckInputFolder()");

                //Debug input folder
                if (!existsInputFolder)
                    DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Input folder doesnt exist"));
                else
                {
                    //Check pc folder
                    if (!existsPCFolder)
                        DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "PC folder doesnt exist"));
                    else
                    {
                        //Check mouse folder
                        if (!existsMouseFolder)
                            DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Mouse folder doesnt exist"));

                        //Check keyboard folder
                        if (!existsKeyboardFolder)
                            DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "Keyboard folder doesnt exist"));
                    }
                }
            }
#endif

            return areInputFoldersOkay;
        }
    }
}
#endif