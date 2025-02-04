﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
 * Copyright (c) 2021 All Rights Reserved.
 * Description：IPolygonGeometrySourceParameters
 * Author： Chance_写代码的厨子
 * Create Time：2021-06-04 17:18:08
 */
namespace CookPopularControl.Expression.Media
{
	public interface IPolygonGeometrySourceParameters : IGeometrySourceParameters
	{
		double PointCount { get; }

		double InnerRadius { get; }
	}
}
