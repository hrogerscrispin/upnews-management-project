import { News } from "../models/new_schema.js";


// Obtener todas las noticias
export const getAllNews = async (res) => {
  try {
    const noticias = await News.find()
      .populate('autorId', 'nombre')
      .populate('categoriaId', 'nombreCategoria')
      .populate('paisId', 'nombrePais')
      .populate('estadoId','codigo')
      .sort({ fechaPublicacion: -1 });

       const data = noticias.map(n=>({
        id: n.id,
        titulo: n.titulo,
        descripcion: n.descripcion,
        contenido: n.contenido,
        portada: n.portada,
        autor: n.autorId?.nombre,
        categoria: n.categoriaId?.nombreCategoria,
        pais: n.paisId?.nombrePais,
        estado: n.estadoId?.codigo,
        fechaPublicacion: n.fechaPublicacion
      }));

    res.status(200).json({
      success: true,
      message: 'Noticias obtenidas correctamente',
      data
    });
  } catch (error) {
    console.error('Error al obtener noticias:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener noticias',
      error: error.message
    });
  }
};

// Obtener noticia por ID
export const getNewsById = async (req, res) => {
  try {
    const { id } = req.params;
    const noticia = await News.findById(id)
      .populate('autorId', 'nombre')
      .populate('categoriaId', 'nombreCategoria')
      .populate('paisId', 'nombrePais')
      .populate('estadoId','codigo');

      const data = noticia.map(n=>({
        id: n.id,
        titulo: n.titulo,
        descripcion: n.descripcion,
        contenido: n.contenido,
        portada: n.portada,
        autor: n.autorId?.nombre,
        categoria: n.categoriaId?.nombreCategoria,
        pais: n.paisId?.nombrePais,
        estado: n.estadoId?.codigo,
        fechaPublicacion: n.fechaPublicacion
      }));

    if (!noticia) {
      return res.status(404).json({
        success: false,
        message: 'Noticia no encontrada'
      });
    }

    res.status(200).json({
      success: true,
      message: 'Noticia obtenida correctamente',
      data
    });
  } catch (error) {
    console.error('Error al obtener noticia:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener noticia',
      error: error.message
    });
  }
};

// Obtener noticias por categoría
export const getNewsByCategory = async (req, res) => {
  try {
    const { categoriaId } = req.params;
    const noticias = await News.find({ categoriaId })
      .populate('autorId', 'nombre correo')
      .populate('categoriaId', 'nombreCategoria')
      .populate('paisId', 'nombrePais')
      .populate('estadoId','codigo')
      .sort({ fechaPublicacion: -1 });

    res.status(200).json({
      success: true,
      message: 'Noticias por categoría obtenidas correctamente',
      data: noticias
    });
  } catch (error) {
    console.error('Error al obtener noticias por categoría:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener noticias por categoría',
      error: error.message
    });
  }
};

// Obtener noticias por país
export const getNewsByCountry = async (req, res) => {
  try {
    const { paisId } = req.params;
    const noticias = await News.find({ paisId })
      .populate('autorId', 'nombre correo')
      .populate('categoriaId', 'nombreCategoria')
      .populate('paisId', 'nombrePais')
      .populate('estadoId','codigo')
      .sort({ fechaPublicacion: -1 });

    res.status(200).json({
      success: true,
      message: 'Noticias por país obtenidas correctamente',
      data: noticias
    });
  } catch (error) {
    console.error('Error al obtener noticias por país:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener noticias por país',
      error: error.message
    });
  }
};


// todo: create more filtering functions to get the news