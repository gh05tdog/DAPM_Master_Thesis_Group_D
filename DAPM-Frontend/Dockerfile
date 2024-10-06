# Stage 1: Build the app
FROM node:18-alpine AS builder

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json to workdir
COPY package*.json ./

# Install dependencies using npm
RUN npm install

# Copy the rest of your app's source code to the container's workspace
COPY . .

# Build the application
RUN npm run build

# Stage 2: Serve the app using nginx
FROM nginx:1.19.0

# Set the working directory for Nginx
WORKDIR /usr/share/nginx/html

# Remove any existing files in the html directory
RUN rm -rf ./*

# Copy the built assets from the build stage to the appropriate nginx folder for static files
COPY --from=builder /app/build .

# Expose port 80 to the Docker host, so we can access it from the outside
EXPOSE 80

# Start Nginx and keep it running in the foreground
ENTRYPOINT ["nginx", "-g", "daemon off;"]
