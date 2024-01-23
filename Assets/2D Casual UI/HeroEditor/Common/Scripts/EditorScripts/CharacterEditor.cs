using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.Common.Scripts.Collections;
using Assets.HeroEditor.Common.Scripts.Common;
using Assets.HeroEditor.Common.Scripts.Data;
using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using Assets.HeroEditor.InventorySystem.Scripts;
using Assets.HeroEditor.InventorySystem.Scripts.Data;
using Assets.HeroEditor.InventorySystem.Scripts.Elements;
using Assets.HeroEditor4D.SimpleColorPicker.Scripts;
using HeroEditor.Common;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.HeroEditor.Common.Scripts.EditorScripts
{
    /// <summary>
    /// Character editor UI and behaviour.
    /// </summary>
    public class CharacterEditor : CharacterEditorBase
    {
        [Header("Public")]
        public SpriteCollection SpriteCollection;
        public IconCollection IconCollection;
        public Transform Tabs;
        public ScrollInventory Inventory;
        public Text ItemName;

        [Header("Other")]
        public List<Toggle> EditionToggles;
        public string EditionFilter;
        public List<string> PaintParts;
        public Button PaintButton;
        public ColorPicker ColorPicker;
        public string FilePickerPath;

        [Header("Import/Export")]
        public List<string> ImportParts;
        public Button ImportButton;
        public Button ExportButton;

        public Action<Item> EquipCallback;

        public static string CharacterJson;

        private Toggle ActiveTab => Tabs.GetComponentsInChildren<Toggle>().Single(i => i.isOn);
        
        /// <summary>
        /// Called automatically on app start.
        /// </summary>
        public void Awake()
        {
            ItemCollection.Active = ScriptableObject.CreateInstance<ItemCollection>();
            ItemCollection.Active.SpriteCollections = new List<SpriteCollection> { SpriteCollection };
            ItemCollection.Active.IconCollections = new List<IconCollection> { IconCollection };
        }

        public new void Start()
        {
            base.Start();
            OnSelectTab(true);
            FilePickerPath = Application.dataPath;
        }

        /// <summary>
        /// This can be used as an example for building your own inventory UI.
        /// </summary>
        public void OnSelectTab(bool value)
        {
            if (value)
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            Action<Item> equipAction;
            int equippedIndex;
            var tab = ActiveTab;

            ItemCollection.Active.Reset();

            switch (tab.name)
            {
                case "Helmet":
                {
                    ItemCollection.Active.Items = SpriteCollection.Helmet.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.Helmet);
                    equippedIndex = Character.Helmet == null ? -1 : SpriteCollection.Helmet.FindIndex(i => i.Sprite == Character.Helmet);
                    break;
                }
                case "Armor":
                {
                    ItemCollection.Active.Items = SpriteCollection.Armor.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.Armor);
                    equippedIndex = Character.Armor == null ? -1 : SpriteCollection.Armor.FindIndex(i => i.Sprites.SequenceEqual(Character.Armor));
                    break;
                }
                case "Pauldrons":
                case "Vest":
                case "Gloves":
                case "Belt":
                case "Boots":
                {
                    string part;

                    switch (tab.name)
                    {
                        case "Pauldrons": part = "ArmR"; break;
                        case "Vest": part = "Torso"; break;
                        case "Gloves": part = "SleeveR"; break;
                        case "Belt": part = "Pelvis"; break;
                        case "Boots": part = "Shin"; break;
                        default: throw new NotSupportedException(tab.name);
                    }

                    ItemCollection.Active.Items = SpriteCollection.Armor.Select(i => CreateFakeItemParams(new Item(i.Id), i, ".Armor.", $".{tab.name}.")).ToList();
                    equipAction = item => Character.Equip(item.Sprite, tab.name.ToEnum<EquipmentPart>());
                    equippedIndex = Character.Armor == null ? -1 : SpriteCollection.Armor.FindIndex(i => i.Sprites.Contains(Character.Armor.SingleOrDefault(j => j.name == part)));
                    break;
                }
                case "Shield":
                {
                    ItemCollection.Active.Items = SpriteCollection.Shield.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.Shield);
                    equippedIndex = SpriteCollection.Shield.FindIndex(i => i.Sprite == Character.Shield);
                    break;
                }
                case "Melee1H":
                {
                    ItemCollection.Active.Items = SpriteCollection.MeleeWeapon1H.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.MeleeWeapon1H);
                    equippedIndex = SpriteCollection.MeleeWeapon1H.FindIndex(i => i.Sprite == Character.PrimaryMeleeWeapon);
                    break;
                }
                case "Melee2H":
                {
                    ItemCollection.Active.Items = SpriteCollection.MeleeWeapon2H.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.MeleeWeapon2H);
                    equippedIndex = SpriteCollection.MeleeWeapon2H.FindIndex(i => i.Sprite == Character.PrimaryMeleeWeapon);
                    break;
                }
                case "MeleePaired":
                {
                    ItemCollection.Active.Items = SpriteCollection.MeleeWeapon1H.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.MeleeWeaponPaired);
                    equippedIndex = SpriteCollection.MeleeWeapon1H.FindIndex(i => i.Sprite == Character.SecondaryMeleeWeapon);
                    break;
                }
                case "Bow":
                {
                    ItemCollection.Active.Items = SpriteCollection.Bow.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.Bow);
                    equippedIndex = Character.Bow == null ? -1 : SpriteCollection.Bow.FindIndex(i => i.Sprites.SequenceEqual(Character.Bow));
                    break;
                }
                case "Firearm1H":
                {
                    ItemCollection.Active.Items = SpriteCollection.Firearm1H.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item =>
                    {
                        if (item.Id == null)
                        {
                            Character.Equip(null, EquipmentPart.MeleeWeapon1H);
                        }
                        else
                        {
                            var itemName = item.Id.Split('.')[3];

                            Character.GetFirearm().Params = FindFirearmParams(itemName);
                            Character.Equip(item.Sprite, EquipmentPart.Firearm1H);
                        }
                       
                        Character.Animator.SetBool("Ready", true);
                    };
                    equippedIndex = Character.Firearms == null ? -1 : SpriteCollection.Firearm1H.FindIndex(i => i.Sprites.SequenceEqual(Character.Firearms));
                    break;
                }
                case "Firearm2H":
                {
                    ItemCollection.Active.Items = SpriteCollection.Firearm2H.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item =>
                    {
                        if (item.Id == null)
                        {
                            Character.Equip(null, EquipmentPart.MeleeWeapon2H);
                        }
                        else
                        {
                            var itemName = item.Id.Split('.')[3];

                            Character.GetFirearm().Params = FindFirearmParams(itemName);
                            Character.Equip(item.Sprite, EquipmentPart.Firearm2H);
                        }
                        
                        Character.Animator.SetBool("Ready", true);
                    };
                    equippedIndex = Character.Firearms == null ? -1 : SpriteCollection.Firearm2H.FindIndex(i => i.Sprites.SequenceEqual(Character.Firearms));
                    break;
                }
                case "Cape":
                {
                    ItemCollection.Active.Items = SpriteCollection.Cape.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.Cape);
                    equippedIndex = SpriteCollection.Cape.FindIndex(i => i.Sprite == Character.Cape);
                    break;
                }
                case "Back":
                {
                    ItemCollection.Active.Items = SpriteCollection.Back.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.Back);
                    equippedIndex = SpriteCollection.Back.FindIndex(i => i.Sprite == Character.Back);
                    break;
                }
                case "Body":
                {
                    ItemCollection.Active.Items = SpriteCollection.Body.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.SetBody(item.Sprite, BodyPart.Body);
                    equippedIndex = Character.Body == null ? -1 : SpriteCollection.Body.FindIndex(i => i.Sprites.SequenceEqual(Character.Body));
                    break;
                }
                case "Head":
                {
                    ItemCollection.Active.Items = SpriteCollection.Head.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.SetBody(item.Sprite, BodyPart.Head);
                    equippedIndex = SpriteCollection.Head.FindIndex(i => i.Sprite == Character.Head);
                    break;
                }
                case "Ears":
                {
                    ItemCollection.Active.Items = SpriteCollection.Ears.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.SetBody(item.Sprite, BodyPart.Ears);
                    equippedIndex = SpriteCollection.Ears.FindIndex(i => i.Sprite == Character.Ears);
                    break;
                }
                case "Eyebrows":
                {
                    ItemCollection.Active.Items = SpriteCollection.Eyebrows.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.SetBody(item.Sprite, BodyPart.Eyebrows);
                    equippedIndex = SpriteCollection.Eyebrows.FindIndex(i => i.Sprite == Character.Expressions[0].Eyebrows);
                    break;
                }
                case "Eyes":
                {
                    ItemCollection.Active.Items = SpriteCollection.Eyes.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.SetBody(item.Sprite, BodyPart.Eyes);
                    equippedIndex = SpriteCollection.Eyes.FindIndex(i => i.Sprite == Character.Expressions[0].Eyes);
                    break;
                }
                case "Hair":
                {
                    ItemCollection.Active.Items = SpriteCollection.Hair.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.SetBody(item.Sprite, BodyPart.Hair);
                    equippedIndex = SpriteCollection.Hair.FindIndex(i => i.Sprite == Character.Hair);
                    break;
                }
                case "Beard":
                {
                    ItemCollection.Active.Items = SpriteCollection.Beard.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.SetBody(item.Sprite, BodyPart.Beard);
                    equippedIndex = SpriteCollection.Beard.FindIndex(i => i.Sprite == Character.Beard);
                    break;
                }
                case "Mouth":
                {
                    ItemCollection.Active.Items = SpriteCollection.Mouth.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.SetBody(item.Sprite, BodyPart.Mouth);
                    equippedIndex = SpriteCollection.Mouth.FindIndex(i => i.Sprite == Character.Expressions[0].Mouth);
                    break;
                }
                //case "Makeup":
                //    {
                //        dict = SpriteCollection.Makeup.ToDictionary(i => i.FullName, i => i);
                //        equipAction = item => Character.SetBody(item.Sprite, BodyPart.Makeup);
                //        equippedIndex = SpriteCollection.Makeup.FindIndex(i => i.Sprite == Character.Makeup);
                //        break;
                //    }
                case "Earrings":
                {
                    ItemCollection.Active.Items = SpriteCollection.Earrings.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.Earrings);
                    equippedIndex = SpriteCollection.Earrings.FindIndex(i => i.Sprite == Character.Earrings);
                    break;
                }
                case "Glasses":
                {
                    ItemCollection.Active.Items = SpriteCollection.Glasses.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.Glasses);
                    equippedIndex = SpriteCollection.Glasses.FindIndex(i => i.Sprite == Character.Glasses);
                    break;
                }
                case "Mask":
                {
                    ItemCollection.Active.Items = SpriteCollection.Mask.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => Character.Equip(item.Sprite, EquipmentPart.Mask);
                    equippedIndex = SpriteCollection.Mask.FindIndex(i => i.Sprite == Character.Mask);
                    break;
                }
                case "Supplies":
                {
                    ItemCollection.Active.Items = SpriteCollection.Supplies.Select(i => CreateFakeItemParams(new Item(i.Id), i)).ToList();
                    equipAction = item => { if (item.Id != null) Debug.LogWarning("Supplies are present as icons only and are not displayed on a character. Can be used for inventory."); };
                    equippedIndex = -1;
                    break;
                }
                default: throw new NotImplementedException(tab.name);
            }

            var items = ItemCollection.Active.Items.Select(i => new Item(i.Id)).ToList();
            var emptyItem = new Item(null);

            items.Insert(0, emptyItem);
            ItemCollection.Active.Items.Add(CreateFakeItemParams(emptyItem, null));

            InventoryItem.OnLeftClick = item =>
            {
                equipAction?.Invoke(item);
                EquipCallback?.Invoke(item);
                ItemName.text = item == emptyItem ? emptyItem.Id : item.Params.SpriteId;
                SetPaintButton(tab.name, item);
            };

            var equipped = items.Count > equippedIndex + 1 ? items[equippedIndex + 1] : null;

            if (EditionFilter != "" && EditionFilter != "Megapack")
            {
                var dict = SpriteCollection.GetAllSprites().ToDictionary(i => i.Id, i => i.Edition);

                if (EditionFilter == "UndeadHeroes")
                {
                    items.RemoveAll(i => i.Id != null && dict[i.Id] != EditionFilter);
                }
                else
                {
                    items.RemoveAll(i => i.Id != null && dict[i.Id] != "Common" && dict[i.Id] != EditionFilter);
                }

                if (equipped != null)
                {
                    if (items.Contains(equipped))
                    {
                        equippedIndex = items.IndexOf(equipped) - 1;
                    }
                    else
                    {
                        equipped = null;
                        equippedIndex = -1;
                    }
                }
            }

            Inventory.Initialize(ref items, items[equippedIndex + 1], reset: true);
            SetPaintButton(tab.name, equipped);

            if (ImportButton) ImportButton.interactable = ImportParts.Contains(tab.name);
        }

        private ItemParams CreateFakeItemParams(Item item, ItemSprite itemSprite, string replaceable = null, string replacement = null)
        {
            var spriteId = itemSprite?.Id;
            var iconId = itemSprite?.Id;

            if (iconId != null && item.Id != null && replaceable != null && replacement != null)
            {
                iconId = iconId.Replace(replaceable, replacement);
            }

            return new ItemParams { Id = item.Id, IconId = iconId, SpriteId = spriteId, Meta = itemSprite == null ? null : Serializer.Serialize(itemSprite.Tags) };
        }

        private void SetPaintButton(string tab, Item item)
        {
            var tags = item?.Params.MetaToList() ?? new List<string>();

            PaintButton.interactable = PaintParts.Contains(tab) && !tags.Contains("NoPaint") || tags.Contains("Paint");
        }

        /// <summary>
        /// Remove all equipment.
        /// </summary>
        public void Reset()
        {
            Character.ResetEquipment();

            var appearance = new CharacterAppearance();

            if (Character.SpriteCollection.Id == "UndeadHeroes")
            {
                appearance.Hair = null;
                appearance.Ears = null;
                appearance.Eyebrows = null;
                appearance.Eyes = "UndeadHeroes.Skeletons.Eyes.Skeleton2EyesGlow";
                appearance.Mouth = null;
                appearance.Head = "UndeadHeroes.Skeletons.Head.Skeleton1";
            }

            appearance.Setup(Character);
            Refresh();
        }

        /// <summary>
        /// Randomize character.
        /// </summary>
        public void Randomize()
        {
            Character.Randomize();
            OnSelectTab(true);
        }

        /// <summary>
	    /// Save character to json.
	    /// </summary>
	    public void SaveToJson()
	    {
            StartCoroutine(StandaloneFilePicker.SaveFile("Save as JSON", "", "New character", "json", Encoding.Default.GetBytes(Character.ToJson()), (success, path) => { Debug.Log(success ? $"Saved as {path}" : "Error saving."); }));
		}

		/// <summary>
		/// Load character from json.
		/// </summary>
		public void LoadFromJson()
	    {
            StartCoroutine(StandaloneFilePicker.OpenFile("Open as JSON", "", "json", (success, path, bytes) =>
            {
                if (success)
                {
                    var json = System.IO.File.ReadAllText(path);

                    Character.FromJson(json);
                }
            }));
	    }

        #if UNITY_EDITOR

        /// <summary>
        /// Save character to prefab.
        /// </summary>
        public void Save()
        {
            var path = UnityEditor.EditorUtility.SaveFilePanel("Save character prefab (should be inside Assets folder)", FilePickerPath, "New character", "prefab");

            if (path.Length > 0)
            {
                if (!path.Contains("/Assets/")) throw new Exception("Unity can save prefabs only inside Assets folder!");

                Save("Assets" + path.Replace(Application.dataPath, null));
                FilePickerPath = path;
            }
		}

	    /// <summary>
		/// Load character from prefab.
		/// </summary>
		public void Load()
        {
            var path = UnityEditor.EditorUtility.OpenFilePanel("Load character prefab", FilePickerPath, "prefab");

            if (path.Length > 0)
            {
                Load("Assets" + path.Replace(Application.dataPath, null));
                FilePickerPath = path;
            }
		}

	    public override void Save(string path)
		{
			Character.transform.localScale = Vector3.one;

			#if UNITY_2018_3_OR_NEWER

			UnityEditor.PrefabUtility.SaveAsPrefabAsset(Character.gameObject, path);

			#else

			UnityEditor.PrefabUtility.CreatePrefab(path, Character.gameObject);

			#endif

            Debug.LogFormat("Prefab saved as {0}", path);
        }

        public override void Load(string path)
        {
			var character = UnityEditor.AssetDatabase.LoadAssetAtPath<Character>(path);

            Character.GetFirearm().Params = character.Firearm.Params; // TODO: Workaround
			Load(character);
            Character.GetComponent<CharacterBodySculptor>().OnCharacterLoaded(character);
        }

	    #else

        public override void Save(string path)
        {
            throw new NotSupportedException();
        }

        public override void Load(string path)
        {
            throw new NotSupportedException();
        }

        #endif

        /// <summary>
        /// Load a scene by name.
        /// </summary>
        public void LoadScene(string sceneName)
        {
            #if UNITY_EDITOR

            if (!UnityEditor.EditorBuildSettings.scenes.Any(i => i.path.Contains(sceneName) && i.enabled))
            {
	            UnityEditor.EditorUtility.DisplayDialog("Hero Editor", $"Please add '{sceneName}.scene' to Build Settings!", "OK");
				return;
            }

            #endif

            if (sceneName == "QUICK START")
            {
                QuickStart.ReturnSceneName = SceneManager.GetActiveScene().name;
            }
            else if (sceneName.StartsWith("TestRoom"))
            {
                TestRoom.ReturnSceneName = SceneManager.GetActiveScene().name;
            }

            CharacterJson = Character.ToJson();
            SceneManager.LoadScene(sceneName);
		}

        /// <summary>
		/// Navigate to URL.
		/// </summary>
		public void Navigate(string url)
        {
            #if UNITY_WEBGL && !UNITY_EDITOR

            Application.ExternalEval($"window.open('{url}')");

            #else

			Application.OpenURL(url);

            #endif
        }

		protected override void SetFirearmParams(ItemSprite entry)
        {
            if (entry == null) return;

            Character.GetFirearm().Params = FindFirearmParams(entry.Name);
		}

        private Color _color;

        public void OpenColorPicker()
        {
            var currentColor = ResolveParts(ActiveTab.name).FirstOrDefault()?.color ?? Color.white;

            ColorPicker.Color = _color = currentColor;
            ColorPicker.OnColorChanged = Paint;
            ColorPicker.SetActive(true);
        }

        public void CloseColorPicker(bool apply)
        {
            if (!apply) Paint(_color);

            ColorPicker.SetActive(false);
        }

        public void Paint(Color color)
        {
            foreach (var part in ResolveParts(ActiveTab.name))
            {
                part.color = color;
                part.sharedMaterial = color == Color.white ? DefaultMaterial : ActiveTab.name == "Eyes" ? EyesPaintMaterial : EquipmentPaintMaterial;
            }

            if (ActiveTab.name == "Eyes")
            {
                Character.Expressions[0].EyesColor = Character.Expressions[1].EyesColor = color;
            }
        }

        public void SetFullHair()
        {
            Character.FullHair = !Character.FullHair;
            Character.HairRenderer.maskInteraction = Character.FullHair ? SpriteMaskInteraction.None : SpriteMaskInteraction.VisibleInsideMask;
        }

        public void OnEditionChanged(bool value)
        {
            if (!value) return;

            foreach (var toggle in EditionToggles)
            {
                if (toggle.isOn) EditionFilter = toggle.name;
            }

            Refresh();
        }

        public void ImportEquipment()
        {
            StartCoroutine(StandaloneFilePicker.OpenFile($"Select {ActiveTab.name}", "", "png", (success, path, bytes) =>
            {
                if (success)
                {
                    var texture = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: false) { filterMode = FilterMode.Bilinear };

                    texture.LoadImage(bytes);
                    texture.PremultiplyAlpha();

                    Sprite CreateSprite(Texture2D texture2d, Sprite reference)
                    {
                        return Sprite.Create(texture2d, reference.rect, new Vector2(reference.pivot.x / reference.rect.width, reference.pivot.y / reference.rect.height), reference.pixelsPerUnit);
                    }

                    switch (ActiveTab.name)
                    {
                        case "Body": Character.BodyRenderers.ForEach(i => i.sprite = CreateSprite(texture, i.sprite)); break;
                        case "Helmet": Character.HelmetRenderer.sprite = CreateSprite(texture, Character.HelmetRenderer.sprite); break;
                        case "Armor": Character.ArmorRenderers.ForEach(i => i.sprite = CreateSprite(texture, i.sprite)); break;
                        case "Shield": Character.ShieldRenderer.sprite = CreateSprite(texture, Character.ShieldRenderer.sprite); break;
                        case "Melee1H": Character.PrimaryMeleeWeaponRenderer.sprite = CreateSprite(texture, Character.PrimaryMeleeWeaponRenderer.sprite); break;
                        case "Melee2H": Character.PrimaryMeleeWeaponRenderer.sprite = CreateSprite(texture, Character.PrimaryMeleeWeaponRenderer.sprite); break;
                        case "Bow": Character.BowRenderers.ForEach(i => i.sprite = CreateSprite(texture, i.sprite)); break;
                        default: throw new NotImplementedException();
                    }
                }
            }));
        }

        public void ExportEquipment()
        {
            Sprite sprite;

            switch (ActiveTab.name)
            {
                case "Helmet": sprite = Character.Helmet; break;
                case "Armor":
                case "Vest":
                case "Pauldrons":
                case "Gloves":
                case "Belt":
                case "Boots": sprite = Character.Armor[0]; break;
                case "Shield": sprite = Character.Shield; break;
                case "Melee1H":
                case "Melee2H": sprite = Character.PrimaryMeleeWeapon; break;
                case "MeleePaired": sprite = Character.SecondaryMeleeWeapon; break;
                case "Bow": sprite = Character.Bow[0]; break;
                case "Back": sprite = Character.Back; break;
                case "Cape": sprite = Character.Cape; break;
                case "Firearm1H":
                case "Firearm2H": sprite = Character.Firearms[0]; break;
                case "Body": sprite = Character.Body[0]; break;
                case "Head": sprite = Character.Head; break;
                case "Hair": sprite = Character.Hair; break;
                case "Beard": sprite = Character.Beard; break;
                case "Eyebrows": sprite = Character.Expressions[0].Eyebrows; break;
                case "Eyes": sprite = Character.Expressions[0].Eyes; break;
                case "Ears": sprite = Character.Ears; break;
                case "Mouth": sprite = Character.Expressions[0].Mouth; break;
                case "Earrings": sprite = Character.Earrings; break;
                case "Mask": sprite = Character.Mask; break;
                case "Glasses": sprite = Character.Glasses; break;
                default: throw new NotImplementedException();
            }

            var tmp = RenderTexture.GetTemporary(sprite.texture.width, sprite.texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);

            Graphics.Blit(sprite.texture, tmp);

            var previous = RenderTexture.active;

            RenderTexture.active = tmp;

            var myTexture2D = new Texture2D(sprite.texture.width, sprite.texture.height);

            myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            myTexture2D.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(tmp);

            var bytes = myTexture2D.EncodeToPNG();
            
            StartCoroutine(StandaloneFilePicker.SaveFile($"Save {ActiveTab.name}", "", ItemName.text, "png", bytes, (success, path) => { Debug.Log(success ? $"Saved as {path}" : "Error saving."); }));
        }

        private static FirearmParams FindFirearmParams(string weaponName)
        {
            foreach (var collection in FirearmCollection.Instances.Values)
            {
                var found = collection.Firearms.SingleOrDefault(i => i.Name == weaponName);

                if (found != null) return found;
            }

            throw new Exception($"Can't find firearm params for {weaponName}.");
        }

        protected override void FeedbackTip()
	    {
			#if UNITY_EDITOR

		    var success = UnityEditor.EditorUtility.DisplayDialog("Hero Editor", "Hi! Thank you for using my asset! I hope you enjoy making your game with it. The only thing I would ask you to do is to leave a review on the Asset Store. It would be awesome support for my asset, thanks!", "Review", "Later");
			
			RequestFeedbackResult(success);

			#endif
	    }
    }
}