
using UnityEngine;

namespace Syrus.Plugins.ChartEditor
{
	/// <summary>
	/// Sets a new proportion for the rect.
	/// </summary>
	internal class ChartBoundsOption : ChartOption
	{
		/// <summary>
		/// Scale rate values.
		/// </summary>
		float minX, maxX, minY, maxY;
		public ChartBoundsOption(float minX, float maxX, float minY, float maxY) : base(0)
		{
			(this.minX, this.maxX, this.minY, this.maxY) = (minX, maxX, minY, maxY);
		}

		public override void ApplyOption()
		{
			GUIChartEditor.CurrentChart.minX = minX;
			GUIChartEditor.CurrentChart.maxX = maxX;
			GUIChartEditor.CurrentChart.minY = minY;
			GUIChartEditor.CurrentChart.maxY = maxY;
			GUIChartEditor.CurrentChart.origin =
				GUIChartEditor.CurrentChart.coordinatesProcessor(Mathf.Abs(minX), Mathf.Abs(minY));
			GUIChartEditor.CurrentChart.userDefinedRect = new Rect(minX, minY,
				Mathf.Abs(maxX - minX), Mathf.Abs(maxY - minY));
		}
	}
}
