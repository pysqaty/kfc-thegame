using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class AnimatorParameterAttribute : System.Attribute
{
    public AnimatorParameterAttribute(string parameterName)
    {
        this.ParameterName = parameterName;
    }

    public string ParameterName { get; }

    public static string GetParameterName(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        AnimatorParameterAttribute[] attributes =
              (AnimatorParameterAttribute[])fi.GetCustomAttributes(
              typeof(AnimatorParameterAttribute), false);
        return (attributes.Length > 0) ? attributes[0].ParameterName : value.ToString();
    }
}