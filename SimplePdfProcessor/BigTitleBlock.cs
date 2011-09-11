/*
 * Created by SharpDevelop.
 * User: aleksey
 * Date: 11.09.2011
 * Time: 22:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace SimplePdfProcessor
{
	/// <summary>
	/// Description of BigTitleBlock.
	/// </summary>
	public class BigTitleBlock: TitleBlock
	{
		public BigTitleBlock()
			: base() {}
		
		public BigTitleBlock(int dx, int dy)
			: base(dx, dy) {}
		
		public BigTitleBlock(int dx, int dy, int width, int height)
			: base(dx, dy, width, height) {}
		
		public override void FormTitleBlockFields()
		{
			this.TitleBlockFields = new List<int[]>
			{
				new int[]{0, 0, 50, 15},
				new int[]{35, 15, 15, 10},
				new int[]{0, 30, 120, 25},
				new int[]{120, 0, 65, 55},
				new int[]{0, 55, Width, Height-55}
			};
		}
	}
}
