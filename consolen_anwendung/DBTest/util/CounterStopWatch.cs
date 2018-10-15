/*
 * Created by SharpDevelop.
 * User: user
 * Date: 15.10.2018
 * Time: 22:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Diagnostics;

using System;

namespace DBTest
{
	namespace util
	{
		/// <summary>
		/// Eine Stopwatch die auch zählen kann und eine vereinfachte Ausgabemethode hat.
		/// (zwb: Kleine c# Übung)
		/// </summary>
		public class CounterStopWatch : Stopwatch
		{
			private int counter = 0;
			
			public CounterStopWatch()
			{
			}
			
			/**
			 * Reset Methode der Basisklasse überschreiben, um sie durch den Counter zu erweitern.
			 */
			public new void Reset()
			{
				counter = 0;
				base.Reset();
			}
			
			public void ResetAndStart()
			{
				Reset();
				Start();
			}
		
			public override string ToString()
			{
				if(counter != 0)
				{
					return ElapsedMilliseconds.ToString() + " (ms) / " + counter + " operations / " + ElapsedMilliseconds/counter + " (ms/op)";
				}
				else 
				{
					return ElapsedMilliseconds.ToString() + " (ms) / " + counter + " operations";
				}
			}
			
			/**
			 * Definieren des ++-Operators: counter inkrementieren!
			 */
			public static CounterStopWatch operator ++(CounterStopWatch w)
			{
			    w.counter++;
			    return w;
			}
		}
	}
}