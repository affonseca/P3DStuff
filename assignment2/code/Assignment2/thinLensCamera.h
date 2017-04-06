#ifndef ThinLensCamera_H
#define ThinLensCamera_H

#include "glm\vec3.hpp"
#include "sampler.h"
#include "ray.h"
#include "Camera.h"

class ThinLensCamera: public Camera
{

private:
	Sampler* samplerDOF;
	float lensRadius;
	float viewPlaneDistance;
	float zoom;

public:
	ThinLensCamera(Sampler * samplerAA, Sampler * samplerDOF, float lensRadius, float focusDistance, float viewPlaneDistance, float zoom);
	void setView(glm::vec3 eye, glm::vec3 center, glm::vec3 up);
	Ray calculatePrimaryRay(int x, int y, glm::vec2 offset);

private:
	glm::vec3 computeRayDirection(glm::vec2 pixelPoint, glm::vec2 lensPoint);
	

};

#endif