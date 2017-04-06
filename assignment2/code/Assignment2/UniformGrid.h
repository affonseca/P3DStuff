#pragma once

#include <vector>

#include "glm\vec3.hpp"
#include "boundingBox.h"
#include "object.h"

typedef struct {
	std::vector<Object*> objects;
	BoundingBox boundingBox;
} GridCell;

class UniformGrid
{
public:
	UniformGrid(float mFactor, std::vector<Object*> objects);
	bool rayCast(Ray ray, glm::vec3& targetPoint, Object*& targetObject);
	GridCell getCell(int x, int y, int z);

private:
	int nObjects;
	glm::ivec3 nCells;
	glm::vec3 worldSize;
	BoundingBox boundingBox;
	std::vector<GridCell> cells;

};