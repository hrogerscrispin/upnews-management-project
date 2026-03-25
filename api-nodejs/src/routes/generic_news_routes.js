import express from "express";
import { getAllNews, getNewsById, getNewsByCategory, getNewsByCountry } from "../controllers/news_controller.js";

const router = express.Router();

// router.post("/", createNews);                          // POST /api/noticias - crear noticia
router.get("/", getAllNews);                           // GET /api/noticias - obtener todas
router.get("/categoria/:categoriaId", getNewsByCategory);  // GET /api/noticias/categoria/:id
router.get("/pais/:paisId", getNewsByCountry);            // GET /api/noticias/pais/:id
router.get("/:id", getNewsById);                       // GET /api/noticias/:id - obtener por ID

export default router;
