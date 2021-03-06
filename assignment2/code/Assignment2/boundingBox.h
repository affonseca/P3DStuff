#pragma once

#include <vector>
#include "glm\vec3.hpp"
#include "ray.h"

class BoundingBox {

protected:
	glm::vec3 minPos, maxPos;

public:
	BoundingBox();
	
	glm::vec3 getMinPos();
	glm::vec3 getMaxPos();

	bool isInside(glm::vec3 point);

	void setPoints(glm::vec3 min, glm::vec3 max);
	bool getIntersection(Ray ray, float& tProx, float& tDist, glm::vec3& tMin, glm::vec3& tMax);


private:
	bool intersectionLoop(float& tMin, float& tMax, float& tProx,
		float& tDist, float v0, float vd, float vMin, float vMax);

};