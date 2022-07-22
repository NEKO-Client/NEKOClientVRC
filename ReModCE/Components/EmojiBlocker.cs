using NEKOClientCore.Core;
using NEKOClientCore.Core.Managers;
using NEKOClientCore.Core.UI.QuickMenu;

namespace NEKOClient.Components
{
    internal class EmojiBlocker : ModComponent
    {
        private static ConfigValue<bool> EmojisEnabled;
        private ReMenuToggle _emojiToggle;


        public EmojiBlocker()
        {
            EmojisEnabled = new ConfigValue<bool>(nameof(EmojisEnabled), true);
            EmojisEnabled.OnValueChanged += () => _emojiToggle?.Toggle(EmojisEnabled);

            NEKOClient.Harmony.Patch(typeof(VRCPlayer).GetMethod(nameof(VRCPlayer.SpawnEmojiRPC)),
                GetLocalPatch(nameof(SpawnEmojiRPCPatch)));
        }

        public override void OnUiManagerInit(UiManager uiManager)
        {
            var menu = uiManager.MainMenu.GetCategoryPage(Page.PageNames.Utility).AddCategory("Emojis");
            _emojiToggle = menu.AddToggle("Enable", "Disable Emojis from players", EmojisEnabled);
        }

        private static bool SpawnEmojiRPCPatch()
        {
            return EmojisEnabled;
        }
    }
}
