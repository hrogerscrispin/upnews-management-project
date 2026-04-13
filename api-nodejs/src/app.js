import express from 'express';
import userRoutes from './routes/user_routes.js';
import countryRoutes from './routes/country_routes.js';
import categoryRoutes from './routes/news_routes.js';
import newsRoutes from './routes/generic_news_routes.js';
import statusRoutes from './routes/status_routes.js'


const app = express();

app.use(express.json());

// Definir rutas
app.use('/api/usuarios', userRoutes);
app.use('/api/paises', countryRoutes);
app.use('/api/categorias', categoryRoutes);
app.use('/api/noticias', newsRoutes);
app.use('/api/estadosNoticia', statusRoutes)

// Middlewares
//app.use('/public',express.static(`${__dirname}/storage/imgs`))


export {app}