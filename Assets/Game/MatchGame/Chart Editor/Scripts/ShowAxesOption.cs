

using UnityEngine;

namespace Syrus.Plugins.ChartEditor
{
	/// <summary>
	/// Shows axes marking the origin of the reference frame.
	/// </summary>
	internal class ShowAxesOption : ChartOption
	{
		private Color axesColor;
		public ShowAxesOption(Color axesColor) : base(2)
		{
			this.axesColor = axesColor;
		}

		public override void ApplyOption()
		{
			GUIChartEditor.CurrentChart.showAxes = true;
			GUIChartEditor.CurrentChart.axesColor = axesColor;
		}
	}
}
