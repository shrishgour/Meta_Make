

namespace Syrus.Plugins.ChartEditor
{
	/// <summary>
	/// Allows to set the origin of the reference frame in the chart.
	/// </summary>
	internal class SetOriginOption : ChartOption
	{
		ChartOrigins originType;
		public SetOriginOption(ChartOrigins originType) : base(1)
		{
			this.originType = originType;
		}

		public override void ApplyOption()
		{
			if (originType == ChartOrigins.BottomLeft)
				GUIChartEditor.CurrentChart.coordinatesProcessor =
					GUIChartEditor.CurrentChart.BottomLeftOrigin;
			else if (originType == ChartOrigins.TopLeft)
				GUIChartEditor.CurrentChart.coordinatesProcessor =
					GUIChartEditor.CurrentChart.TopLeftOrigin;
		}
	}

	/// <summary>
	/// The available origins for the reference frame.
	/// </summary>
	public enum ChartOrigins
	{
		TopLeft,
		BottomLeft
	}
}

