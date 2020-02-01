module.exports = {
    module: {
        rules: [
            {
                test: /\.(js)$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                }
            }
        ]
    },
    entry: {
        app: './wwwroot/js/index.js'
    },
    output: {
        filename: 'bundle.js',
        path: __dirname + '/wwwroot'
    }
};