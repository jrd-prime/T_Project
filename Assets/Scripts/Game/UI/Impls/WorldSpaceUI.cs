using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Game.UI.Impls
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(UIDocument))]
    public sealed class WorldSpaceUI : MonoBehaviour
    {
        #region fields

        private const string KTransparentShader = "Unlit/Transparent";
        private const string KTextureShader = "Unlit/Texture";
        private const string KMainTex = "_MainTex";

        private static readonly int MainTex = Shader.PropertyToID(KMainTex);

        [SerializeField] private int panelWidth = 1280;
        [SerializeField] private int panelHeight = 720;
        [SerializeField] private float panelScale = 1f;
        [SerializeField] private float pixelsPerUnit = 500f;
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        [SerializeField] private PanelSettings _panelSettingsAsset;
        [SerializeField] private RenderTexture _renderTextureAsset;

        private MeshRenderer _meshRenderer;
        private UIDocument _uiDocument;
        private PanelSettings _panelSettings;
        private RenderTexture _renderTexture;
        private Material _material;

        #endregion

        public void SetLabelText(string label, string text)
        {
            if (_uiDocument.rootVisualElement == null)
            {
                _uiDocument.visualTreeAsset = _visualTreeAsset;
            }

            _uiDocument.rootVisualElement.Q<Label>(label).text = text;
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
            _uiDocument.visualTreeAsset = _visualTreeAsset;
        }

        private void CreatePanelSettings()
        {
            _panelSettings = Instantiate(_panelSettingsAsset);
            _panelSettings.targetTexture = _renderTexture;
            _panelSettings.clearColor = true;
            _panelSettings.scaleMode = PanelScaleMode.ConstantPixelSize;
            _panelSettings.scale = panelScale;
            _panelSettings.name = "Panel Settings";
        }

        private void CreateRenderTexture()
        {
            RenderTextureDescriptor descriptor = _renderTextureAsset.descriptor;
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

        private Mesh GetQuadMesh()
        {
            var tempQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            var quadMesh = tempQuad.GetComponent<MeshFilter>().sharedMesh;
            Destroy(tempQuad);
            return quadMesh;
        }
    }
}
