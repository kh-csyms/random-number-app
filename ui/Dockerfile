# Start from the official Node.js LTS base image
FROM node:lts-alpine

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy local code to the container image
COPY . .

# Build the application
RUN npm run build

EXPOSE 80

# Run the web service on container startup
CMD [ "npm", "start" ]
