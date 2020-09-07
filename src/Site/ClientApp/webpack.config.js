var path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
var LiveReloadPlugin = require('webpack-livereload-plugin');

module.exports = {
    entry: {
        app: './src/index.js',
        home: './src/pages/home.js',
        product: './src/pages/product.js'
    },
    plugins: [
        new CleanWebpackPlugin(),
        new LiveReloadPlugin({
            protocol: 'https'
        })
    ],
    output: {
        filename: '[name].bundle.js',
        path: path.resolve(__dirname, '../wwwroot/shop')
    },
    watchOptions: {
        aggregateTimeout: 200,
        poll: 1000
    },
    module: {
        rules: [
            {
                test: /\.css$/i,
                use: [
                    'style-loader',
                    'css-loader'
                ]
            },
            {
                test: /\.js/,
                use: 'babel-loader'
            },
            {
                test: /\.ico/,
                loader: 'file-loader',
                options: {
                    name: '[name].[ext]'
                }
            },
            {
                test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                loader: 'file-loader',
                options: {
                    name: 'fonts/[name].[ext]',
                    publicPath: 'shop'
                }
            },
            {
                test: /\.(png|jpe?g|gif)$/i,
                loader: 'file-loader',
                options: {
                    name: 'img/[name].[ext]',
                    publicPath: 'shop'
                }
            }
        ]
    }
};