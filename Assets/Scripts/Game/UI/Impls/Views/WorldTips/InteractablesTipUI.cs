using Infrastructure.Input.Common;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Views.WorldTips
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(UIDocument))]
    public sealed class InteractablesTipUI : MonoBehaviour
    {
        #region fields

        private const string KTransparentShader = "Unlit/Transparent";
        private const string KTextureShader = "Unlit/Texture";
        private const string KMainTex = "_MainTex";

        private const string NameLabelId = "name";
        private const string TipLabelId = "tip";

        private static readonly int MainTex = Shader.PropertyToID(KMainTex);

        [SerializeField] private int panelWidth = 1600;
        [SerializeField] private int panelHeight = 900;
        [SerializeField] private float panelScale = 1f;
        [SerializeField] private float pixelsPerUnit = 500f;
        [SerializeField] private VisualTreeAsset visualTreeAsset;
        [SerializeField] private PanelSettings panelSettingsAsset;
        [SerializeField] private RenderTexture renderTextureAsset;

        private MeshRenderer _meshRenderer;
        private UIDocument _uiDocument;
        private PanelSettings _panelSettings;
        private RenderTexture _renderTexture;
        private Material _material;
        private Label _nameLabel;
        private Label _tipLabel;

        #endregion


        public void SetTipText((string, string) textTuple)
        {
            if (_uiDocument.rootVisualElement == null) _uiDocument.visualTreeAsset = visualTreeAsset;

            _nameLabel.text = textTuple.Item1;
            _tipLabel.text = $"{textTuple.Item2} ({KeyName.Interact})";
        }

        private void OnEnable()
        {
            _nameLabel = _uiDocument.rootVisualElement.Q<Label>(NameLabelId);
            _tipLabel = _uiDocument.rootVisualElement.Q<Label>(TipLabelId);
        }

        private void Awake()
        {
            InitComponents();
            BuildPanel();
        }

        private void BuildPanel()
        {
            CreateRenderTexture();
            CreatePanelSettings();
            CreateUIDocument();
            CreateMaterial();

            SetMaterialToRenderer();
            SetPanelSize();
        }

        private void SetMaterialToRenderer()
        {
            if (_meshRenderer != null) _meshRenderer.sharedMaterial = _material;
        }

        private void SetPanelSize()
        {
            if (_renderTexture != null && (_renderTexture.width != panelWidth || _renderTexture.height != panelHeight))
            {
                _renderTexture.Release();
                _renderTexture.width = panelWidth;
                _renderTexture.height = panelHeight;
                _renderTexture.Create();

                _uiDocument?.rootVisualElement?.MarkDirtyRepaint();
            }

            transform.localScale = new Vector3(panelWidth / pixelsPerUnit, panelHeight / pixelsPerUnit, 1f);
        }

        private void CreateMaterial()
        {
            var shaderName = _panelSettings.colorClearValue.a < 1f ? KTransparentShader : KTextureShader;
            _material = new Material(Shader.Find(shaderName));
            _material.SetTexture(MainTex, _renderTexture);
        }

        private void CreateUIDocument()
        {
            _uiDocument = GetComponent<UIDocument>();
            _uiDocument.panelSettings = _panelSettings;
            _uiDocument.visualTreeAsset = visualTreeAsset;
        }

        private void CreatePanelSettings()
        {
            _panelSettings = Instantiate(panelSettingsAsset);
            _panelSettings.targetTexture = _renderTexture;
            _panelSettings.clearColor = true;
            _panelSettings.scaleMode = PanelScaleMode.ConstantPixelSize;
            _panelSettings.scale = panelScale;
            _panelSettings.name = "Panel Settings";
        }

        private void CreateRenderTexture()
        {
            RenderTextureDescriptor descriptor = renderTextureAsset.descriptor;
            descriptor.width = panelWidth;
            descriptor.height = panelHeight;

            _renderTexture = new RenderTexture(descriptor)
            {
                name = "UI Render Texture"
            };
        }

        private void InitComponents()
        {
            InitMeshRenderer();

            MeshFilter meshFilter = GetComponent<MeshFilter>();
            meshFilter.sharedMesh = GetQuadMesh();
        }

        private void InitMeshRenderer()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.sharedMaterial = null;
            _meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            _meshRenderer.receiveShadows = false;
            _meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            _meshRenderer.lightProbeUsage = LightProbeUsage.Off;
            _meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
        }

        private static Mesh GetQuadMesh()
        {
            var tempQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            var quadMesh = tempQuad.GetComponent<MeshFilter>().sharedMesh;
            Destroy(tempQuad);
            return quadMesh;
        }
    }
}
