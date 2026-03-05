import { coreApiInstance } from "./axiosInstance";

/* ---------------- PROFESSION ---------------- */

export const getProfessions = async () => {
  const response = await coreApiInstance.get(
    "/MasterData/profession-drop-down"
  );

  return response.data;
};

/* ---------------- SKILL ---------------- */

export const getSkills = async () => {
  const response = await coreApiInstance.get(
    "/MasterData/skill-drop-down"
  );

  return response.data;
};

/* ---------------- DESIGNATION ---------------- */

export const getDesignations = async (professionId: number) => {
  const response = await coreApiInstance.get(
    `/MasterData/designation-dropdown/${professionId}`
  );

  return response.data;
};