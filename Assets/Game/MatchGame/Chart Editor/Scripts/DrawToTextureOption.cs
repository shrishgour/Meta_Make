
using UnityEngine;

namespace Syrus.Plugins.ChartEditor
{
	internal class DrawToTextureOption : ChartOption
	{
		/// <summary>
		/// The texture the graph will be drawn on.
		/// </summary>
		private Texture2D texture;
        private TextureSettings texSettings;

        public DrawToTextureOption(Texture2D texture) : base(3)
		{
			this.texture = texture;
		}
		public DrawToTextureOption(Texture2D texture, TextureSettings settings) : base(3)
		{
			this.texture = texture;
            this.texSettings = settings;
		}

		public override void ApplyOption()
		{
			RenderTexture rt = RenderTexture.GetTemporary(texture.width, texture.height);
			RenderTexture.active = rt;

			// By setting an output texture we tell GUIChartEditor to draw on it in
			// GUIChartEditor.EndChart().
			GUIChartEditor.CurrentChart.outputTexture = texture;

            // Tell the GUIChartEditor what render settings to apply to the texture.
            GUIChartEditor.CurrentChart.outputTextureSettings = this.texSettings;
		}
	}

    public struct TextureSettings
    {
        /// <summary>
        /// The filtering applied to the texture when rendered.
        /// </summary>
        public FilterMode filtering;

        /// <summary>
        /// Specifies how much a generated texture will be compressed.
        /// </summary>
        public TextureCompression compression;
    }

    /// <summary>
    /// Specifies how much a generated texture will be compressed.
    /// </summary>
    public enum TextureCompression : byte
    {
        /// <summary>
        /// Texture will be rendered perfectly, at the cost of slowest render speed.
        /// </summary>
        None,

        /// <summary>
        /// Texture will have a few color artifacts that are hard to notice. Best for general purposes.
        /// </summary>
        HighQuality,

        /// <summary>
        /// Texture will have a lot of color artifacts, with the benefit of fast rendering. Good for far-away objects.
        /// </summary>
        LowQuality
    }
}
