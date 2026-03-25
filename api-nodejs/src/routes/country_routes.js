import express from "express";
import { getAllCountries, getCountryById } from "../controllers/country_controller.js";

const router = express.Router();

//router.post("/", createCountry);           // POST /api/paises - crear país
router.get("/", getAllCountries);          // GET /api/paises - obtener todos
router.get("/:id", getCountryById);        // GET /api/paises/:id - obtener por ID

export default router;
