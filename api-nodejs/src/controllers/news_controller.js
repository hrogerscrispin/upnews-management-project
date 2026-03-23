import { News } from "../models/new_schema.js";

// Crear una nueva noticia
// export const createNews = async (req, res) => {
//   try {
//     const { titulo, descripcion, contenido, portada, autorId, categoriaId, paisId } = req.body;

//     if (!titulo || !descripcion || !contenido || !autorId || !categoriaId || !paisId) {
//       return res.status(400).json({
//         success: false,
//         message: 'Faltan campos requeridos: titulo, descripcion, contenido, autorId, categoriaId, paisId'
//       });
//     }

//     const nuevaNoticia = new News({
//       titulo,
//       descripcion,
//       contenido,
//       portada,
//       autorId,
//       categoriaId,
//       paisId
//     });

//     await nuevaNoticia.save();

//     const noticia = await News.findById(nuevaNoticia._id)
//       .populate('autorId', 'nombre correo')
//       .populate('categoriaId', 'nombreCategoria')
//       .populate('paisId', 'nombrePais');

//     res.status(201).json({
//       success: true,
//       message: 'Noticia creada correctamente',
//       data: noticia
//     });
//   } catch (error) {
//     console.error('Error al crear noticia:', error);
//     res.status(500).json({
//       success: false,
//       message: 'Error al crear noticia',
//       error: error.message
//     });
//   }
// };

// Obtener todas las noticias
export const getAllNews = async (req, res) => {
  try {
    const noticias = await News.find()
      .populate('autorId', 'nombre correo')
      .populate('categoriaId', 'nombreCategoria')
      .populate('paisId', 'nombrePais')
      .sort({ fechaPublicacion: -1 });

    res.status(200).json({
      success: true,
      message: 'Noticias obtenidas correctamente',
      data: noticias
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
      .populate('autorId', 'nombre correo')
      .populate('categoriaId', 'nombreCategoria')
      .populate('paisId', 'nombrePais');

    if (!noticia) {
      return res.status(404).json({
        success: false,
        message: 'Noticia no encontrada'
      });
    }

    res.status(200).json({
      success: true,
      message: 'Noticia obtenida correctamente',
      data: noticia
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
