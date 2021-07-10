float _DistortionSize;
sampler2D _Albedo;
float4 _AlbedoTint;
sampler2D _Metallic;
float _Smoothness;
sampler2D _NormalMap;
float4 _GlowColor;
float _GlowWidth;
sampler2D _NoiseTex;
float _MinY;
float _MaxY;
float _AnimTime;
float _InverseDirection;

struct teleportData
{
    float alpha : ALPHA;
    int emission : EMISSION;
};

float remap(float num, float2 rangeIn, float2 rangeOut)
{
    float inWidth = rangeIn.y - rangeIn.x;
    float outWidth = rangeOut.y - rangeOut.x;
    float percentage = (num - rangeIn.x) / inWidth;
    return rangeOut.x + percentage * outWidth;
}

teleportData getTeleportData(float3 worldPos)
{
    teleportData o;
    float4 noiseSample = tex2D(_NoiseTex, worldPos.xz);
    float distVal = remap(noiseSample.x, float2(0, 1), float2(-1, 1)) * _DistortionSize;

    if (_InverseDirection)
    {
        _AnimTime = 1 - _AnimTime;
    }

    float edge = distVal + _AnimTime;
    if (_AnimTime == 0 || _AnimTime == 1)
    {
        edge = _AnimTime;
    }

    float heightPercentage = remap(worldPos.y, float2(_MinY, _MaxY), float2(1, 0));

    if (_InverseDirection)
    {
        edge *= -1;
        heightPercentage *= -1;
    }
    
    float glowWidth = _AnimTime == 0 ? 0 : _GlowWidth;
    float glowHeight = heightPercentage - glowWidth;



    float glowVal = edge - glowHeight;


    o.alpha = 1;
    if (edge > heightPercentage)
    {
        o.alpha = 0;
    }
    o.emission = 0;
    if (glowVal > 0)
    {
        o.emission = 1;
    }
    return o;
}