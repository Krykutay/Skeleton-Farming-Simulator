//BLURS-------------------------------------------------------------------------
half4 Blur(half2 uv, sampler2D source, half Intensity)
{
	half step = 0.00390625f * Intensity;
	half4 result = half4 (0, 0, 0, 0);
	half2 texCoord = half2(0, 0);
	texCoord = uv + half2(-step, -step);
	result += tex2D(source, texCoord);
	texCoord = uv + half2(-step, 0);
	result += 2.0 * tex2D(source, texCoord);
	texCoord = uv + half2(-step, step);
	result += tex2D(source, texCoord);
	texCoord = uv + half2(0, -step);
	result += 2.0 * tex2D(source, texCoord);
	texCoord = uv;
	result += 4.0 * tex2D(source, texCoord);
	texCoord = uv + half2(0, step);
	result += 2.0 * tex2D(source, texCoord);
	texCoord = uv + half2(step, -step);
	result += tex2D(source, texCoord);
	texCoord = uv + half2(step, 0);
	result += 2.0* tex2D(source, texCoord);
	texCoord = uv + half2(step, -step);
	result += tex2D(source, texCoord);
	result = result * 0.0625;
	return result;
}
half BlurHD_G(half bhqp, half x)
{
	return exp(-(x * x) / (2.0 * bhqp * bhqp));
}
half4 BlurHD(half2 uv, sampler2D source, half Intensity)
{
	int iterations = 16;
	int halfIterations = iterations / 2;
	half sigmaX = 0.1 + Intensity * 0.5;
	half sigmaY = sigmaX;
	half total = 0.0;
	half4 ret = half4(0, 0, 0, 0);
	for (int iy = 0; iy < iterations; ++iy)
	{
		half fy = BlurHD_G(sigmaY, half(iy) -half(halfIterations));
		half offsety = half(iy - halfIterations) * 0.00390625;
		for (int ix = 0; ix < iterations; ++ix)
		{
			half fx = BlurHD_G(sigmaX, half(ix) - half(halfIterations));
			half offsetx = half(ix - halfIterations) * 0.00390625;
			total += fx * fy;
			ret += tex2D(source, uv + half2(offsetx, offsety)) * fx * fy;
		}
	}
	return ret / total;
}
//-----------------------------------------------------------------------
half rand(half2 seed, half offset) {
	return (frac(sin(dot(seed, half2(12.9898, 78.233))) * 43758.5453) + offset) % 1.0;
}

half rand2(half2 seed, half offset) {
	return (frac(sin(dot(seed * floor(50 + (_Time % 1.0) * 12.), half2(127.1, 311.7))) * 43758.5453123) + offset) % 1.0;
}
//-----------------------------------------------------------------------
half RemapFloat(half inValue, half inMin, half inMax, half outMin, half outMax){
	return outMin + (inValue - inMin) * (outMax - outMin) / (inMax - inMin);
}