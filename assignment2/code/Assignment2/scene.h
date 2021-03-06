#ifndef SCENE_H
#define SCENE_H

#include <vector>

#include "uniformGrid.h"
#include "camera.h"
#include "thinLensCamera.h"
#include "light.h"
#include "material.h"
#include "object.h"
#include "glm\vec3.hpp"

class Scene {

private:

	glm::vec3* backgroundColor;
	Camera* camera;
	bool useSoftShadows;
	std::vector<Light*>* lights;
	std::vector<Material*>* materials;
	std::vector<Object*>* objects;

	UniformGrid* uniformGrid;
	float uniformGridM;

public:
	Scene(int numSamples, bool useDOF, ThinLensParameters thinLensParameters, bool useSoftShadows, float uniformGridM);
	Scene(int numSamples, bool useDOF, bool useSoftShadows, float uniformGridM);
	~Scene();

	glm::vec3* getBackgroundColor();
	Camera* getCamera();
	std::vector<Light*>* getLights();
	std::vector<Material*>* getMaterials();
	std::vector<Object*>* getObjects();

	UniformGrid* getUniformGrid();

	bool loadSceneFromNFF(char* path);
	bool rayCast(Ray ray, glm::vec3& targetPoint, Object*& targetObject);

};


#endif;