 // This asset was uploaded by https://unityassetcollection.com

using UnityEngine;

namespace Syrus.Plugins.ChartEditor
{
	internal class ShowLabelsOption : ChartOption
	{
		/// <summary>
		/// The coordinates + value of the labels to show.
		/// </summary>
		private float[] labels;

		/// <summary>
		/// The string that defines the formatting rule for labels.
		/// </summary>
		private string format;

		public ShowLabelsOption(string format, params float[] labels) : base(3)
		{
			if (labels.Length % 3 != 0)
			{
				Debug.LogError("ShowLabels requires a multiple of 3 parameters.");
				return;
			}
			this.format = format;
			this.labels = labels;
		}

		public override void ApplyOption()
		{
			for (int i = 0; i < labels.Length; i += 3)
				GUIChartEditor.PushValueLabel(labels[i], labels[i + 1], labels[i + 2], format);
		}
	}
}
