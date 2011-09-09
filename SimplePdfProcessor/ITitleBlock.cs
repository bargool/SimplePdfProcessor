/*
 * Created by SharpDevelop.
 * User: aleksey
 * Date: 09.09.2011
 * Time: 22:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SimplePdfProcessor
{
	/// <summary>
	/// Description of ITitleBlock.
	/// </summary>
	public interface ITitleBlock
	{
		public int pageIndex {get; set;}
		int dX;
		int dY;
		int Width;
		int Height;

		public void Draw();
	}
}
