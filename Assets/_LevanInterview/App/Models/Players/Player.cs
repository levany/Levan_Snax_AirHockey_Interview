using System;
using System.Collections;
using System.Collections.Generic;
using LevanInterview.Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevanInterview.Models
{
	[CreateAssetMenu(fileName="Player", menuName="Models/Player", order=0)]
	[Serializable]
	public class Player : Model
	{
		[Header("Info")]
		public string	  Name;
		public int		  Score;

		[Header("Gameplay")]
		public Color	  Color;

    } 
}
